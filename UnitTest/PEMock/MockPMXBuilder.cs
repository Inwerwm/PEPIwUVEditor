using PEPlugin;
using PEPlugin.Pmx;
using PEPlugin.SDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.PEMock
{
    public class MockPMXBuilder : IPXPmxBuilder
    {
        public IPXBody Body()
        {
            throw new NotImplementedException();
        }

        public IPXBone Bone()
        {
            throw new NotImplementedException();
        }

        public IPXBoneMorphOffset BoneMorphOffset()
        {
            throw new NotImplementedException();
        }

        public IPXBoneMorphOffset BoneMorphOffset(IPXBone bone, V3 translation, Q rotation)
        {
            throw new NotImplementedException();
        }

        public IPXBoneNodeItem BoneNodeItem()
        {
            throw new NotImplementedException();
        }

        public IPXBoneNodeItem BoneNodeItem(IPXBone bone)
        {
            throw new NotImplementedException();
        }

        public IPXFace Face()
        {
            throw new NotImplementedException();
        }

        public IPXGroupMorphOffset GroupMorphOffset()
        {
            throw new NotImplementedException();
        }

        public IPXGroupMorphOffset GroupMorphOffset(IPXMorph morph, float ratio)
        {
            throw new NotImplementedException();
        }

        public IPXIKLink IKLink()
        {
            throw new NotImplementedException();
        }

        public IPXIKLink IKLink(IPXBone bone)
        {
            throw new NotImplementedException();
        }

        public IPXIKLink IKLink(IPXBone bone, V3 low, V3 high)
        {
            throw new NotImplementedException();
        }

        public IPXImpulseMorphOffset ImpulseMorphOffset()
        {
            throw new NotImplementedException();
        }

        public IPXImpulseMorphOffset ImpulseMorphOffset(IPXBody body, bool local, V3 velocity, V3 torque)
        {
            throw new NotImplementedException();
        }

        public IPXJoint Joint()
        {
            throw new NotImplementedException();
        }

        public IPXMaterial Material()
        {
            throw new NotImplementedException();
        }

        public IPXMaterialMorphOffset MaterialMorphOffset()
        {
            throw new NotImplementedException();
        }

        public IPXMorph Morph()
        {
            return new MockMorph();
        }

        public IPXMorphNodeItem MorphNodeItem()
        {
            throw new NotImplementedException();
        }

        public IPXMorphNodeItem MorphNodeItem(IPXMorph morph)
        {
            throw new NotImplementedException();
        }

        public IPXNode Node()
        {
            throw new NotImplementedException();
        }

        public IPXPmx Pmx()
        {
            return new MockPmx();
        }

        public IPXSoftBody SoftBody()
        {
            throw new NotImplementedException();
        }

        public IPXSoftBodyAnchor SoftBodyAnchor()
        {
            throw new NotImplementedException();
        }

        public IPXUVMorphOffset UVMorphOffset()
        {
            throw new NotImplementedException();
        }

        public IPXUVMorphOffset UVMorphOffset(IPXVertex vertex, V4 offset)
        {
            return new MockUVMorphOffset(vertex, offset);
        }

        public IPXVertex Vertex()
        {
            throw new NotImplementedException();
        }

        public IPXVertexMorphOffset VertexMorphOffset()
        {
            throw new NotImplementedException();
        }

        public IPXVertexMorphOffset VertexMorphOffset(IPXVertex vertex, V3 offset)
        {
            throw new NotImplementedException();
        }
    }
}
