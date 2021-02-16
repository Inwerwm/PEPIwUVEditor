using IwUVEditor.DirectX;
using System;
using System.Collections.Generic;

namespace IwUVEditor.Tool
{
    class ToolBox : IDisposable
    {
        private bool disposedValue;

        internal SlimDX.Direct3D11.Device Device { get; set; }
        UVViewDrawProcess Process { get; set; }

        Dictionary<Type, IEditTool> ToolOf { get; }

        public ToolBox()
        {
            ToolOf = new Dictionary<Type, IEditTool>();
        }

        T CallTool<T>(Func<T> constructor, UVViewDrawProcess process) where T : IEditTool
        {
            IEditTool tool;
            if (!ToolOf.TryGetValue(typeof(T), out tool))
                ToolOf.Add(typeof(T), null);

            if (tool == null || Process != process)
            {
                ToolOf[typeof(T)] = constructor();
                Process = process;
            }

            return (T)ToolOf[typeof(T)];
        }

        public RectangleSelection RectangleSelection(UVViewDrawProcess process)
        {
            return CallTool(() => new RectangleSelection(Device, process.Effect, process.Rasterize.Solid, process.PositionSquares), process);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    foreach (var tool in ToolOf.Values)
                    {
                        tool?.Dispose();
                    }
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~ToolBox()
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
