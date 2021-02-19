using IwUVEditor.Command;
using IwUVEditor.DirectX;
using IwUVEditor.Manager;
using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IwUVEditor.Tool
{
    class MoveVertices : IEditTool
    {
        private bool disposedValue;

        public bool IsReady { get; private set; }

        DirectX.UVViewDrawProcess Process { get; }

        List<IPXVertex> TargetVertices { get; set; }
        Material TargetMaterial => Process.Current.Material;

        Vector2 StartPos { get; set; }
        Vector2 CurrentPos { get; set; }

        Matrix Offset => Matrix.Translation(new Vector3(CurrentPos - StartPos, 0));
        Matrix TotalOffset { get; set; }

        public MoveVertices(UVViewDrawProcess process)
        {
            Process = process;
        }

        public IEditorCommand CreateCommand(Material target)
        {
            IsReady = false;
            return new CommandMoveVertices(TargetVertices, Offset);
        }

        public void PrepareDrawing()
        {
            Process.UpdateDrawingVertices();
        }

        public void ReadInput(DragManager mouse, Dictionary<Keys, bool> pressKey)
        {
            if (mouse.IsStartingJust)
            {
                TotalOffset = Matrix.Identity;
                TargetVertices = TargetMaterial.IsSelected.Where(p => p.Value).Select(p => p.Key).ToList();
                StartPos = mouse.Start;
            }
            if (mouse.IsDragging)
            {
                CurrentPos = mouse.Current;
                TargetVertices.AsParallel().ForAll(vtx => TargetMaterial.TemporaryTransformMatrices[vtx] *= Offset);
                TotalOffset *= Offset;

                // オフセットを得た時点で現在地点を次の初期位置扱いする
                // 評価ごとの移動量にすることで累積が可能になる
                StartPos = CurrentPos;
            }
            if (mouse.IsEndingJust)
            {
                IsReady = true;
                TargetVertices.AsParallel().ForAll(vtx => TargetMaterial.TemporaryTransformMatrices[vtx] *= Matrix.Invert(TotalOffset));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~MoveVertices()
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
    }
}
