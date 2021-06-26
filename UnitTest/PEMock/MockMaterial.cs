using PEPlugin.Pmx;
using PEPlugin.SDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.PEMock
{
    class MockMaterial : IPXMaterial
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string NameE { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public V4 Diffuse { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public V3 Specular { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public V3 Ambient { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Power { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool BothDraw { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Shadow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool SelfShadowMap { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool SelfShadow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool VertexColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public PrimitiveType PrimitiveType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Edge { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public V4 EdgeColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float EdgeSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Tex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sphere { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SphereType SphereMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Toon { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Memo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IList<IPXFace> Faces => throw new NotImplementedException();

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
