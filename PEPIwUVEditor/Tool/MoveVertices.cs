using IwUVEditor.Command;
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

        List<IPXVertex> TargetVertices { get; set; }
        Material TargetMaterial { get; set; }

        Vector2 StartPos { get; set; }
        Vector2 CurrentPos { get; set; }

        Matrix Offset => Matrix.Translation(new Vector3(CurrentPos - StartPos, 0));

        public IEditorCommand CreateCommand(Material target)
        {
            return new CommandMoveVertices();
        }

        public void PrepareDrawing()
        {
            
        }

        public void ReadInput(DragManager mouse, Dictionary<Keys, bool> pressKey)
        {
            if (mouse.IsStartingJust)
            {
                TargetVertices = TargetMaterial.IsSelected.Where(p => p.Value).Select(p => p.Key).ToList();
                StartPos = mouse.Start;
            }
            if (mouse.IsDragging)
            {
                CurrentPos = mouse.Current;
            }
            if (mouse.IsEndDrag)
            {
                IsReady = true;
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
