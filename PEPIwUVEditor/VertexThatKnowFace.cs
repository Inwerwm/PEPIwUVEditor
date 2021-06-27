using PEPlugin.Pmx;
using PEPlugin.SDX;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    class VertexThatKnowFace : IPXVertex
    {
        private IPXVertex Substance { get; }
        public ImmutableList<IPXFace> Parent => ImmutableList.Create(FoundFaces);
        private IPXFace[] FoundFaces { get; set; }

        private VertexThatKnowFace(IPXVertex substance, IPXFace[] foundFaces)
        {
            Substance = substance;
            FoundFaces = foundFaces;
        }

        public static IEnumerable<VertexThatKnowFace> FindFacesFrom(IEnumerable<IPXVertex> vertices, IEnumerable<IPXFace> faces) =>
            vertices.AsParallel().Select(vtx =>
                new VertexThatKnowFace(vtx, faces.Where(face => face.Vertex1 == vtx || face.Vertex2 == vtx || face.Vertex3 == vtx).ToArray())
            );

        #region Interface implement by Substance
        public V3 Position { get => Substance.Position; set => Substance.Position = value; }
        public V3 Normal { get => Substance.Normal; set => Substance.Normal = value; }
        public V2 UV { get => Substance.UV; set => Substance.UV = value; }
        public V4 UVA1 { get => Substance.UVA1; set => Substance.UVA1 = value; }
        public V4 UVA2 { get => Substance.UVA2; set => Substance.UVA2 = value; }
        public V4 UVA3 { get => Substance.UVA3; set => Substance.UVA3 = value; }
        public V4 UVA4 { get => Substance.UVA4; set => Substance.UVA4 = value; }
        public IPXBone Bone1 { get => Substance.Bone1; set => Substance.Bone1 = value; }
        public IPXBone Bone2 { get => Substance.Bone2; set => Substance.Bone2 = value; }
        public IPXBone Bone3 { get => Substance.Bone3; set => Substance.Bone3 = value; }
        public IPXBone Bone4 { get => Substance.Bone4; set => Substance.Bone4 = value; }
        public float Weight1 { get => Substance.Weight1; set => Substance.Weight1 = value; }
        public float Weight2 { get => Substance.Weight2; set => Substance.Weight2 = value; }
        public float Weight3 { get => Substance.Weight3; set => Substance.Weight3 = value; }
        public float Weight4 { get => Substance.Weight4; set => Substance.Weight4 = value; }
        public bool QDEF { get => Substance.QDEF; set => Substance.QDEF = value; }
        public bool SDEF { get => Substance.SDEF; set => Substance.SDEF = value; }
        public V3 SDEF_C { get => Substance.SDEF_C; set => Substance.SDEF_C = value; }
        public V3 SDEF_R0 { get => Substance.SDEF_R0; set => Substance.SDEF_R0 = value; }
        public V3 SDEF_R1 { get => Substance.SDEF_R1; set => Substance.SDEF_R1 = value; }
        public float EdgeScale { get => Substance.EdgeScale; set => Substance.EdgeScale = value; }

        public object Clone()
        {
            return Substance.Clone();
        }
        #endregion
    }
}
