using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace IwUVEditor.Command
{
    class CommandRectangleSelection : IEditorCommand
    {
        Material TargetMaterial { get; }

        RectangleF SelectionRange { get; set; }
        Dictionary<IPXVertex, bool> PreviousState { get; set; }
        Dictionary<IPXVertex, bool> SelectionResult { get; set; }

        public Vector2 StartPosition { get; }
        public Vector2 EndPosition { get; }

        public SelectionMode Mode { get; }

        public CommandRectangleSelection(Material targetMaterial, Vector2 startPosition, Vector2 endPosition, SelectionMode mode)
        {
            TargetMaterial = targetMaterial;
            StartPosition = startPosition;
            EndPosition = endPosition;
            Mode = mode;
        }

        private void CreateSelectionRange()
        {
            var min = new Vector2(Math.Min(StartPosition.X, EndPosition.X), Math.Min(StartPosition.Y, EndPosition.Y));
            var max = new Vector2(Math.Max(StartPosition.X, EndPosition.X), Math.Max(StartPosition.Y, EndPosition.Y));
            var size = max - min;

            SelectionRange = new RectangleF(min.X, min.Y, size.X, size.Y);
        }

        public void Do()
        {
            CreateSelectionRange();

            // 頂点の選択状態を調査
            SelectionResult = TargetMaterial.Vertices.ToDictionary(vtx => vtx, vtx => SelectionRange.Contains(vtx.UV.X, vtx.UV.Y));

            // 現在の選択状態を保存
            PreviousState = TargetMaterial.IsSelected.Select(pair => (pair.Key, pair.Value)).ToDictionary(p => p.Key, p => p.Value);

            // 選択状態を反映
            foreach (var sel in SelectionResult)
            {
                switch (Mode)
                {
                    case SelectionMode.Create:
                        TargetMaterial.IsSelected[sel.Key] = sel.Value;
                        break;
                    case SelectionMode.Union:
                        TargetMaterial.IsSelected[sel.Key] |= sel.Value;
                        break;
                    case SelectionMode.Difference:
                        TargetMaterial.IsSelected[sel.Key] &= !sel.Value;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public void Undo()
        {
            foreach (var state in PreviousState)
            {
                TargetMaterial.IsSelected[state.Key] = state.Value;
            }
        }
    }
}
