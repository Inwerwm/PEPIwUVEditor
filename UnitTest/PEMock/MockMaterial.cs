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
        public string Name { get; set; }
        public string NameE { get; set; }
        public V4 Diffuse { get; set; }
        public V3 Specular { get; set; }
        public V3 Ambient { get; set; }
        public float Power { get; set; }
        public bool BothDraw { get; set; }
        public bool Shadow { get; set; }
        public bool SelfShadowMap { get; set; }
        public bool SelfShadow { get; set; }
        public bool VertexColor { get; set; }
        public PrimitiveType PrimitiveType { get; set; }
        public bool Edge { get; set; }
        public V4 EdgeColor { get; set; }
        public float EdgeSize { get; set; }
        public string Tex { get; set; }
        public string Sphere { get; set; }
        public SphereType SphereMode { get; set; }
        public string Toon { get; set; }
        public string Memo { get; set; }

        public IList<IPXFace> Faces { get; private set; }

        public MockMaterial()
        {
            Faces = new List<IPXFace>();
        }

        public MockMaterial(IList<IPXVertex> vertices, params (int A, int B, int C)[] indices)
        {
            CreateFaces(vertices, indices);
        }

        public void CreateFaces(IList<IPXVertex> vertices, params (int A, int B, int C)[] indices)
        {
            Faces = indices.Select(i => (IPXFace)new MockFace(vertices[i.A], vertices[i.B], vertices[i.C])).ToList();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
