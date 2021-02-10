using SlimDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buffer = SlimDX.Direct3D11.Buffer;

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
