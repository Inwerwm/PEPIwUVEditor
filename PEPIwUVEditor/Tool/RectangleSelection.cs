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

        SelectionRectangle ToDrawRectangle { get; }
        PositionSquares ToUpdateElement { get; }

        SelectionMode SelectionMode { get; set; }

        public RectangleSelection(SelectionRectangle toDrawRectangle, PositionSquares toUpdateElement)
        {
            ToDrawRectangle = toDrawRectangle;
            ToUpdateElement = toUpdateElement;
        }

        public void ReadInput(DragManager mouse, Dictionary<System.Windows.Forms.Keys, bool> pressKey)
        {
            if (mouse.IsStartingJust)
            {
                ToDrawRectangle.StartPos = mouse.Start;
            }

            if (mouse.IsDragging)
            {
                ToDrawRectangle.EndPos = mouse.Current;
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
            return new CommandRectangleSelection(target, ToDrawRectangle.StartPos, ToDrawRectangle.EndPos, SelectionMode, ToUpdateElement.UpdateVertices);
        }

        public void PrepareDrawing()
        {
            if (NeedsDrawing)
                ToDrawRectangle.Prepare();
        }
    }
}
