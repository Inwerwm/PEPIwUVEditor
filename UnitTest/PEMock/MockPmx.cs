using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.PEMock
{
    class MockPmx : IPXPmx
    {
        public IPXHeader Header { get; }

        public IPXModelInfo ModelInfo{get;}

        public IList<IPXVertex> Vertex{get;}

        public IList<IPXMaterial> Material{get;}

        public IList<IPXBone> Bone{get;}

        public IList<IPXMorph> Morph{get;}

        public IPXNode RootNode{get;}

        public IPXNode ExpressionNode{get;}

        public IList<IPXNode> Node{get;}

        public IList<IPXBody> Body{get;}

        public IList<IPXJoint> Joint{get;}

        public IList<IPXSoftBody> SoftBody{get;}

        public IPXPrimitiveBuilder Primitive{get;}

        public string FilePath { get; set; }

        public MockPmx(IList<IPXVertex> vertex)
        {
            Vertex = vertex;
        }

        public MockPmx(IList<IPXVertex> vertex, IList<IPXMaterial> material) : this(vertex)
        {
            Material = material;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void FromFile(string path)
        {
            throw new NotImplementedException();
        }

        public void FromStream(Stream s)
        {
            throw new NotImplementedException();
        }

        public void Normalize()
        {
            throw new NotImplementedException();
        }

        public void ToFile(string path)
        {
            throw new NotImplementedException();
        }

        public void ToStream(Stream s)
        {
            throw new NotImplementedException();
        }
    }
}
