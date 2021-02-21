using IwUVEditor.Command;
using IwUVEditor.DirectX;
using IwUVEditor.Manager;
using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IwUVEditor.Tool
{
    internal abstract class EditVertices
    {
        private bool disposedValue;
        private Vector3 centerPos;

        public bool IsReady { get; private set; }

        protected UVViewDrawProcess Process { get; }

        protected List<IPXVertex> TargetVertices { get; set; }
        protected Material TargetMaterial => Process.Current.Material;

        protected Vector2 StartPos { get; set; }
        protected Vector2 CurrentPos { get; set; }
        protected Vector3 CenterPos
        {
            get => centerPos;
            private set => centerPos = value;
        }

        protected Dictionary<Keys, bool> IsPress { get; private set; }

        protected abstract Matrix Offset { get; }
        protected Matrix TotalOffset { get; set; }

        protected EditVertices(UVViewDrawProcess process)
        {
            Process = process;
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

        public void PrepareDrawing()
        {
            Process.UpdateDrawingVertices();
        }

        public virtual void ReadInput(DragManager mouse, Dictionary<Keys, bool> pressKey)
        {
            IsPress = pressKey;

            if (mouse.IsStartingJust)
            {
                TotalOffset = Matrix.Identity;
                TargetVertices = TargetMaterial.IsSelected.Where(p => p.Value).Select(p => p.Key).ToList();
                CenterPos = CalcCenterPos();
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

        private Vector3 CalcCenterPos()
        {
            Vector2 min = new Vector2(TargetVertices.Min(vtx => vtx.UV.X), TargetVertices.Min(vtx => vtx.UV.Y));
            Vector2 max = new Vector2(TargetVertices.Max(vtx => vtx.UV.X), TargetVertices.Max(vtx => vtx.UV.Y));
            Vector2 center = new Vector2((min.X + max.X) / 2, (min.Y + max.Y) / 2);
            return new Vector3(center, 0);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }
    }
}