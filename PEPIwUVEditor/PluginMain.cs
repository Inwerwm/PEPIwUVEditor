using PEPExtensions;
using PEPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    public class IwUVEditor : IPEPlugin
    {
        private bool disposedValue;

        public string Name => "IwUVEditor";

        public string Version => "1.0";

        public string Description => "UVをGUIで編集する";

        public IPEPluginOption Option => new PEPluginOption(false, true);

        private FormEditor Editor { get; set; }

        public void Run(IPERunArgs args)
        {
            try
            {
                Editor = Editor ?? new FormEditor(args);
                Editor.Visible = true;
                Editor.Initialize();
            }
            catch (Exception ex)
            {
                Utility.ShowException(ex);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    Editor?.Dispose();
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~Class1()
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
