using IwUVEditor.DirectX;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IwUVEditor.Tool
{
    class ToolBox : IDisposable
    {
        private bool disposedValue;
        private UVViewDrawProcess _process;

        internal SlimDX.Direct3D11.Device Device { get; set; }
        UVViewDrawProcess Process
        {
            get => _process;
            set
            {
                _process = value;
                foreach (var tool in ToolInstances)
                {
                    tool.Value?.Dispose();
                }
                ToolInstances.Clear();
            }
        }

        // ツールオブジェクトのキャッシュ
        GenerableMap<Type, IEditTool> ToolInstances { get; }
        /// <summary>
        /// ツールのインスタンスを参照したいとき用
        /// </summary>
        public ReadOnlyDictionary<Type, IEditTool> InstanceOf => new ReadOnlyDictionary<Type, IEditTool>(ToolInstances);

        public ToolBox()
        {
            ToolInstances = new GenerableMap<Type, IEditTool>((_) => null);
        }

        /// <summary>
        /// 型に対応したツールのインスタンスを返す
        /// </summary>
        /// <typeparam name="T">ツールの型</typeparam>
        /// <param name="constructor">ツールのコンストラクタ呼び出し関数</param>
        /// <param name="process">描画プロセス</param>
        /// <returns>指定した型のインスタンス</returns>
        private T CallTool<T>(Func<T> constructor, UVViewDrawProcess process) where T : IEditTool
        {
            // Processが既存で引数と異なれば入れ替え
            // 入れ替えると全ツールが削除される
            if (Process != process)
                Process = process;

            // 呼び出し元の型に対応するインスタンスが存在しなければ生成する
            if (ToolInstances[typeof(T)] == null)
                ToolInstances[typeof(T)] = constructor();

            return (T)ToolInstances[typeof(T)];
        }

        public RectangleSelection RectangleSelection(UVViewDrawProcess process) =>
            CallTool(() => new RectangleSelection(Device, process.Effect, process.Rasterize.Solid), process);

        public MoveVertices MoveVertices(UVViewDrawProcess process) =>
            CallTool(() => new MoveVertices(process), process);

        public RotateVertices RotateVertices(UVViewDrawProcess process) =>
            CallTool(() => new RotateVertices(process), process);

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    foreach (var tool in ToolInstances.Values)
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
        #endregion
    }
}
