using IwUVEditor.Command;
using IwUVEditor.Manager;
using IwUVEditor.StateContainer;
using PEPlugin;
using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IwUVEditor
{
    /// <summary>
    /// エディタ機能クラス
    /// </summary>
    class Editor : IDisposable
    {
        private bool disposedValue;

        // モデル
        IPERunArgs Args { get; }
        IPXPmx Pmx { get; set; }
        public List<Material> Materials { get; private set; }

        // 現在の状態
        public EditorStates Current { get; }
        public bool IsEdited { get; private set; }

        // エディタ機能
        public Tool.ToolBox ToolBox { get; }
        Dictionary<Material, CommandManager> Commanders { get; set; }
        SlimDX.Vector2? PositionClip { get; set; }
        public ObservableEditParameter EditParameters { get; }

        // 描画の更新メソッド
        public Action UpdateDraw { get; set; }
        // エディタのリセットメソッド
        Action Resetter { get; }

        public Editor(IPERunArgs args, EditorStates inputManager, Action resetter)
        {
            Args = args;
            Current = inputManager;
            EditParameters = new ObservableEditParameter();
            ToolBox = new Tool.ToolBox(EditParameters);
            Resetter = resetter;
            IsEdited = false;
        }

        public void LoadModel()
        {
            // モデルを読込
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();

            // 材質を読込
            if (!Pmx.Material.Any())
                throw new Exception("モデルに材質が含まれていません。");
            Materials = Pmx.Material.Select((material, i) => new Material(material, Pmx)).ToList();
            Commanders = Materials.ToDictionary(m => m, _ => new CommandManager());
            Current.Material = Materials.First();
            IsEdited = false;
        }

        public void SendModel()
        {
            PEPExtensions.Utility.Update(Args.Host.Connector, Pmx);
            IsEdited = false;
            UpdateDraw?.Invoke();
        }

        public bool CanContinueClosing()
        {
            //編集されていないなら閉じてもいい
            if (!IsEdited)
                return true;

            var result = System.Windows.Forms.MessageBox.Show(
                $"頂点の編集状態を反映しますか?{Environment.NewLine}" +
                $"反映していない場合、編集は失われます。",
                "UV編集",
                System.Windows.Forms.MessageBoxButtons.YesNoCancel,
                System.Windows.Forms.MessageBoxIcon.Exclamation
            );

            switch (result)
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    // 閉じるのをやめる
                    return false;
                case System.Windows.Forms.DialogResult.Yes:
                    // 反映してから閉じる
                    SendModel();
                    return true;
                default:
                    // そのまま閉じる
                    return true;
            }
        }

        public void Reset()
        {
            if (CanContinueClosing())
                Resetter.Invoke();
        }

        public void DriveTool(InputStates input)
        {
            if (Current.Tool is null)
                return;

            Current.Tool.ReadInput(input);
            if (Current.Tool.IsReady)
                Do(Current.Material, Current.Tool.CreateCommand(Current.Material));
        }

        public void Do(Material targetMaterial, IEditorCommand command)
        {
            if (targetMaterial is null)
                return;

            Commanders[targetMaterial].Do(command);
            // 破壊的変更を行う命令だった場合、編集済みに変更
            IsEdited |= command.IsDestructive;

            UpdateDraw?.Invoke();
        }

        public void Undo()
        {
            if (Current.Material is null)
                return;

            Commanders[Current.Material].Undo();
            UpdateDraw?.Invoke();
        }

        public void Redo()
        {
            if (Current.Material is null)
                return;

            Commanders[Current.Material].Redo();
            UpdateDraw?.Invoke();
        }

        public void CopyPosition()
        {
            var selectedVertices = Current.Material.IsSelected.Where(isSelected => isSelected.Value).Select(v => v.Key);

            //LinqのAverageメソッドだと2回回す必要があるのでforeachで平均を計算する
            PEPlugin.SDX.V2 sum = new PEPlugin.SDX.V2(0, 0);
            int count = 0;
            foreach (var vtx in selectedVertices)
            {
                sum += vtx.UV;
                count++;
            }

            PositionClip = sum / count;
        }

        public void PastePosition()
        {
            if (PositionClip is null)
                return;

            var selectedVertices = Current.Material.IsSelected.Where(isSelected => isSelected.Value).Select(v => v.Key);
            Do(Current.Material, new CommandSetPosition(selectedVertices, PositionClip.Value));
        }

        private void ReverseVertices(CommandReverse.Axis axis)
        {
            var selectedVertices = Current.Material.IsSelected.Where(isSelected => isSelected.Value);
            if (!selectedVertices.Any())
                return;

            Do(Current.Material, new CommandReverse(selectedVertices.Select(selected => selected.Key), axis));
        }

        public void ReverseVerticesHorizontal()
        {
            ReverseVertices(CommandReverse.Axis.X);
        }

        public void ReverseVerticesVertical()
        {
            ReverseVertices(CommandReverse.Axis.Y);
        }

        public void ApplyEditWithValue()
        {
            var selectedVertices = Current.Material.IsSelected.Where(isSelected => isSelected.Value);
            if (!selectedVertices.Any())
                return;

            var trsMat = Matrix.Translation(EditParameters.MoveOffset);
            var rotMat = Matrix.Translation(EditParameters.RotationCenter * -1) * Matrix.RotationZ(EditParameters.RotationAngle) * Matrix.Translation(EditParameters.RotationCenter);
            var sclMat = Matrix.Invert(Matrix.Translation(EditParameters.ScaleCenter)) * Matrix.Scaling(EditParameters.ScaleRatio) * Matrix.Translation(EditParameters.ScaleCenter);

            Do(Current.Material, new CommandApplyVertexEdit(selectedVertices.Select(selected => selected.Key), sclMat * rotMat * trsMat));
        }

        public async Task SelectContinuousVertices()
        {
            Dictionary<IPXVertex, IPXFace[]> vfmap = Current.Material.Vertices.AsParallel().Select(vtx =>
                    (Substance: vtx, Faces: Current.Material.Faces.Where(face => face.Vertex1 == vtx || face.Vertex2 == vtx || face.Vertex3 == vtx).ToArray())
                ).ToDictionary(pair => pair.Substance, pair => pair.Faces);

            var selectedVertices = Current.Material.IsSelected.Where(isSelected => isSelected.Value).Select(selected => selected.Key);
            var selectTarget = new Dictionary<IPXVertex, bool>(Current.Material.IsSelected);
            await Task.WhenAll(selectedVertices.Select(vtx => Task.Run(() => SelectContinuousVertices(vtx, vfmap, selectTarget))));

            Do(Current.Material, new CommandSelectVertices(Current.Material, selectTarget) { Mode = SelectionMode.Union });
        }

        private static Task SelectContinuousVertices(IPXVertex vtx, Dictionary<IPXVertex, IPXFace[]> vfmap, Dictionary<IPXVertex, bool> selectTarget)
        {
            selectTarget[vtx] = true;

            var nextVertices = vfmap[vtx].SelectMany(f => new[] { f.Vertex1, f.Vertex2, f.Vertex3 }.Where(v => v != vtx && !selectTarget[v]));
            return nextVertices.Any() ? Task.WhenAll(nextVertices.Select(v => Task.Run(() => SelectContinuousVertices(v, vfmap, selectTarget)))) : Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    ToolBox?.Dispose();
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~Editor()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        internal void FetchSelectedVertices()
        {
            var selectedVertexIndices = Args.Host.Connector.View.PmxView.GetSelectedVertexIndices();
            var selectedVertices = selectedVertexIndices.Select(i => Pmx.Vertex[i]);
            var selectTargetVertices = selectedVertices.Intersect(Current.Material.Vertices);

            Do(Current.Material, new CommandSelectVertices(Current.Material, selectTargetVertices.ToDictionary(vtx => vtx, _ => true)) { Mode = SelectionMode.Union });
        }

        internal void SendSelectedVertices()
        {
            var selectedVertices = Current.Material.IsSelected.Where(p => p.Value).Select(p => p.Key);
            Args.Host.Connector.View.PmxView.SetSelectedVertexIndices(selectedVertices.AsParallel().Select(v => Pmx.Vertex.IndexOf(v)).ToArray());
        }

        internal void ChangeTexture(string filePath)
        {
            var uri = new Uri(Pmx.FilePath.Replace("%", "%25"));
            var rPath = uri.MakeRelativeUri(new Uri(filePath.Replace("%", "%25")));

            Current.Material.Tex = Uri.UnescapeDataString(rPath.ToString()).Replace("%25", "%");
        }

        internal void ExportUVImage(int imageSize, string exportPath, bool drawTexture)
        {
            var uvMesh = new ExportUV.UVMesh(Current.Material.Vertices, Current.Material.Faces);
            new ExportUV.GDIUVDrawer().Draw(uvMesh, imageSize, Current.Material.TexFullPath, exportPath, drawTexture);
        }
    }
}
