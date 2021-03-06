﻿using IwUVEditor.Command;
using IwUVEditor.DirectX.DrawElement;
using IwUVEditor.Manager;
using IwUVEditor.StateContainer;
using SlimDX;
using System;
using System.Collections.Generic;

namespace IwUVEditor.Tool
{
    class RectangleSelection : IEditTool
    {
        private bool disposedValue;
        private static Color4 rectangleColor = new Color4(0.5f, 1, 1, 1);

        public bool IsReady { get; private set; }
        private bool NeedsDrawing { get; set; }

        SelectionRectangle SelectionRectangle { get; }

        public Color4 RectangleColor
        {
            get => rectangleColor;
            set
            {
                rectangleColor = value;
                SelectionRectangle.Color = rectangleColor;
            }
        }

        SelectionMode SelectionMode { get; set; }

        public RectangleSelection(SlimDX.Direct3D11.Device device, DirectX.UVViewDrawProcess process)
        {
            SelectionRectangle = new SelectionRectangle(device, process.Effect, process.Rasterize.Solid, RectangleColor);
        }

        public void Initialize() { }

        public void ReadInput(InputStates input)
        {
            if (input.MouseLeft.IsStartingJust)
            {
                SelectionRectangle.StartPos = input.MouseLeft.Start;
            }

            if (input.MouseLeft.IsDragging)
            {
                SelectionRectangle.EndPos = input.MouseLeft.Current;
                NeedsDrawing = true;
            }

            if (input.MouseLeft.IsEndingJust)
            {
                SelectionMode = input.IsPress[System.Windows.Forms.Keys.ShiftKey] ? SelectionMode.Union : input.IsPress[System.Windows.Forms.Keys.ControlKey] ? SelectionMode.Difference : SelectionMode.Create;
                NeedsDrawing = false;
                IsReady = true;
                input.MouseLeft.Reset();
            }
        }

        public IEditorCommand CreateCommand(Material target)
        {
            IsReady = false;
            return new CommandRectangleSelection(target, SelectionRectangle.StartPos, SelectionRectangle.EndPos, SelectionMode);
        }

        public void PrepareDrawing()
        {
            if (NeedsDrawing)
                SelectionRectangle.Prepare();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    SelectionRectangle?.Dispose();
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~RectangleSelection()
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
