using PEPlugin.Pmx;
using System.Collections.Generic;

namespace UnitTest.PEMock
{
    internal class MockMorph : IPXMorph
    {
        public string Name { get; set; }
        public string NameE { get; set; }
        public int Panel { get; set; }
        public MorphKind Kind { get; set; }

        public bool IsVertex { get; }

        public bool IsUV { get; }

        public bool IsBone { get; }

        public bool IsMaterial { get; }

        public bool IsGroup { get; }

        public bool IsFlip { get; }

        public bool IsImpulse { get; }

        public IList<IPXMorphOffset> Offsets { get; }

        public MockMorph()
        {
            Offsets = new List<IPXMorphOffset>();
        }

        public object Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}