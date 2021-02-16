using System;

namespace IwUVEditor.DirectX.DrawElement
{
    interface IDrawElement : IDisposable
    {
        /// <summary>
        /// Draw関数で呼ぶ用の関数
        /// </summary>
        void Prepare();
    }
}
