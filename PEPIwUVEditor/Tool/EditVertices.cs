using IwUVEditor.Command;
using IwUVEditor.Controller;
using IwUVEditor.DirectX;
using IwUVEditor.Manager;
using IwUVEditor.StateContainer;
using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IwUVEditor.Tool
{
    internal abstract class EditVertices : IEditTool
    {
        protected bool disposedValue;

        public bool IsReady { get; private set; }

        protected UVViewDrawProcess Process { get; }
        protected InputStates Input { get; set; }
        protected EditController Controller { get; }

        protected List<IPXVertex> TargetVertices { get; set; }
        protected Material TargetMaterial => Process.Current.Material;

        protected Vector2 StartPos { get; set; }
        protected Vector2 CurrentPos { get; set; }
        protected Vector3 CenterPos
        {
            get
            {
                if (!TargetVertices.Any())
                    return new Vector3();

                Vector2 min = new Vector2(TargetVertices.Min(vtx => vtx.UV.X), TargetVertices.Min(vtx => vtx.UV.Y));
                Vector2 max = new Vector2(TargetVertices.Max(vtx => vtx.UV.X), TargetVertices.Max(vtx => vtx.UV.Y));
                Vector2 center = new Vector2((min.X + max.X) / 2, (min.Y + max.Y) / 2);
                return new Vector3(center, 0);
            }
        }

        protected abstract Matrix Offset { get; }
        protected Matrix TotalOffset { get; set; }

        protected EditVertices(UVViewDrawProcess process, EditController controller)
        {
            Process = process;
            Controller = controller;
        }

        public virtual void Initialize() 
        {
            TargetVertices = TargetMaterial.IsSelected.Where(p => p.Value).Select(p => p.Key).ToList();
            Controller.Center = CenterPos;
        }

        public IEditorCommand CreateCommand(Material target)
        {
            IsReady = false;
            return new CommandApplyVertexEdit(TargetVertices, TotalOffset);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void PrepareDrawing()
        {
            Process.UpdateDrawingVertices();
            Controller.PrepareDrawing();
        }

        public virtual void ReadInput(InputStates input)
        {
            Input = input;

            Controller.ReadInput(input, DoEdit);
        }

        private void DoEdit(InputStates input)
        {
            if (!TargetVertices.Any())
                return;

            if (input.MouseLeft.IsStartingJust)
            {
                TargetVertices = TargetMaterial.IsSelected.Where(p => p.Value).Select(p => p.Key).ToList();
                TotalOffset = Matrix.Identity;
                StartPos = input.MouseLeft.Start;
            }
            if (input.MouseLeft.IsDragging)
            {
                // 頂点編集のプレビュー
                // 直接頂点位置を編集するわけにはいかないので見た目だけ変換する
                CurrentPos = input.MouseLeft.Current;
                TargetVertices.AsParallel().ForAll(vtx => TargetMaterial.TemporaryTransformMatrices[vtx] *= Offset);
                TotalOffset *= Offset;

                // オフセットを得た時点で現在地点を次の初期位置扱いする
                // 評価ごとの移動量にすることで累積が可能になる
                StartPos = CurrentPos;
            }
            if (input.MouseLeft.IsEndingJust)
            {
                // コマンド生成が可能であることを申告
                IsReady = true;
                // プレビュー表示のための変換行列を初期化
                TargetVertices.AsParallel().ForAll(vtx => TargetMaterial.TemporaryTransformMatrices[vtx] *= Matrix.Invert(TotalOffset));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Controller?.Dispose();
                }
                disposedValue = true;
            }
        }
    }
}