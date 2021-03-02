using IwUVEditor.DirectX.Vertex;
using SlimDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.DirectX.DrawElement
{
    class ScalingController : DrawElement<VertexStruct>, IDrawElement
    {
        public ScalingController(Device device, EffectPass usingEffectPass, RasterizerState drawMode) : base(device, usingEffectPass, drawMode)
        {
        }

        protected override uint[] CreateIndices()
        {
            throw new NotImplementedException();
        }

        protected override VertexStruct[] CreateVertices()
        {
            throw new NotImplementedException();
        }

        protected override void DrawToDevice()
        {
            throw new NotImplementedException();
        }
    }
}
