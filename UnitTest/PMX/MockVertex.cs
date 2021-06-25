using PEPlugin.Pmx;
using PEPlugin.SDX;

namespace UnitTest.PMX
{
    class MockVertex : IPXVertex
    {
        public V3 Position { get; set; }
        public V3 Normal { get; set; }
        public V2 UV { get; set; }
        public V4 UVA1 { get; set; }
        public V4 UVA2 { get; set; }
        public V4 UVA3 { get; set; }
        public V4 UVA4 { get; set; }
        public IPXBone Bone1 { get; set; }
        public IPXBone Bone2 { get; set; }
        public IPXBone Bone3 { get; set; }
        public IPXBone Bone4 { get; set; }
        public float Weight1 { get; set; }
        public float Weight2 { get; set; }
        public float Weight3 { get; set; }
        public float Weight4 { get; set; }
        public bool QDEF { get; set; }
        public bool SDEF { get; set; }
        public V3 SDEF_C { get; set; }
        public V3 SDEF_R0 { get; set; }
        public V3 SDEF_R1 { get; set; }
        public float EdgeScale { get; set; }

        public MockVertex() { }

        public MockVertex(V2 uv)
        {
            UV = uv;
        }

        public object Clone() => new MockVertex()
        {
            Position = this.Position,
            Normal = this.Normal,
            UV = this.UV,
            UVA1 = this.UVA1,
            UVA2 = this.UVA2,
            UVA3 = this.UVA3,
            UVA4 = this.UVA4,
            Bone1 = this.Bone1,
            Bone2 = this.Bone2,
            Bone3 = this.Bone3,
            Bone4 = this.Bone4,
            Weight1 = this.Weight1,
            Weight2 = this.Weight2,
            Weight3 = this.Weight3,
            Weight4 = this.Weight4,
            QDEF = this.QDEF,
            SDEF = this.SDEF,
            SDEF_C = this.SDEF_C,
            SDEF_R0 = this.SDEF_R0,
            SDEF_R1 = this.SDEF_R1,
            EdgeScale = this.EdgeScale
        };
    }
}
