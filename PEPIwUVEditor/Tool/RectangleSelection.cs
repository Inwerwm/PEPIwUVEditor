using IwUVEditor.Command;
using IwUVEditor.DirectX.DrawElement;
using IwUVEditor.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Tool
{
    class RectangleSelection : IEditTool
    {
        public bool IsReady { get; private set; }
        private bool NeedsDrawing { get; set; }

        SelectionRectangle SelectionRectangle { get; }
        PositionSquares ToUpdateElement { get; }

        SelectionMode SelectionMode { get; set; }

        public RectangleSelection(SlimDX.Direct3D11.Device device, SlimDX.Direct3D11.Effect effect, SlimDX.Direct3D11.RasterizerState drawMode, PositionSquares toUpdateElement)
        {
            SelectionRectangle = new SelectionRectangle(device, effect, drawMode, new SlimDX.Color4(1, 1, 1));
            ToUpdateElement = toUpdateElement;
        }

        public void ReadInput(DragManager mouse, Dictionary<System.Windows.Forms.Keys, bool> pressKey)
        {
            if (mouse.IsStartingJust)
            {
                SelectionRectangle.StartPos = mouse.Start;
            }

            if (mouse.IsDragging)
            {
                SelectionRectangle.EndPos = mouse.Current;
                NeedsDrawing = true;
            }

            if (mouse.IsEndDrag)
            {
                SelectionMode = pressKey[System.Windows.Forms.Keys.ShiftKey] ? SelectionMode.Union : pressKey[System.Windows.Forms.Keys.ControlKey] ? SelectionMode.Difference : SelectionMode.Create;
                NeedsDrawing = false;
                IsReady = true;
                mouse.Reset();
            }
        }

        public IEditorCommand CreateCommand(Material target)
        {
            IsReady = false;
            return new CommandRectangleSelection(target, SelectionRectangle.StartPos, SelectionRectangle.EndPos, SelectionMode, ToUpdateElement.UpdateVertices);
        }

        public void PrepareDrawing()
        {
            if (NeedsDrawing)
                SelectionRectangle.Prepare();
        }
    }
}
