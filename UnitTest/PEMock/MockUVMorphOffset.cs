using PEPlugin.Pmx;
using PEPlugin.SDX;

namespace UnitTest.PEMock
{
    internal class MockUVMorphOffset : IPXUVMorphOffset
    {
        public IPXVertex Vertex { get; set; }
        public V4 Offset { get; set; }

        public MockUVMorphOffset(IPXVertex vertex, V4 offset)
        {
            Vertex = vertex;
            Offset = offset;
        }

        public object Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}