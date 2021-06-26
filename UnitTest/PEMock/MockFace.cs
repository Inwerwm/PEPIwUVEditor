using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.PEMock
{
    class MockFace : IPXFace
    {
        public IPXVertex Vertex1 { get; set; }
        public IPXVertex Vertex2 { get; set; }
        public IPXVertex Vertex3 { get; set; }

        public MockFace(IPXVertex vertex1, IPXVertex vertex2, IPXVertex vertex3)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
