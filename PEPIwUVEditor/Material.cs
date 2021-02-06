﻿using PEPExtensions;
using PEPlugin.Pmx;
using PEPlugin.SDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    class Material : IPXMaterial
    {
        IPXMaterial Value { get; }

        public IList<IPXVertex> Vertices { get; }
        /// <summary>
        /// インデックスバッファに与える面の構成頂点番号配列
        /// </summary>
        public int[] FaceSequence { get; }

        string ModelPath { get; }
        public string TexFullPath => Path.Combine(Path.GetDirectoryName(ModelPath), Tex);

        public Material(IPXMaterial material, IPXPmx pmx)
        {
            Value = material;
            ModelPath = pmx.FilePath;

            IEnumerable<IPXVertex> FaceVertices = Faces.SelectMany(face => face.ToVertices());

            Vertices = FaceVertices.Distinct().ToList();

            var VtxIdDic = Vertices.Select((vtx, i) => (vtx, i)).ToDictionary(pair => pair.vtx, pair => pair.i);
            FaceSequence = FaceVertices.Select(vtx => VtxIdDic[vtx]).ToArray();
        }

        public override string ToString()
        {
            return Name;
        }

        #region IPXMaterial
        public string Name { get => Value.Name; set => Value.Name = value; }
        public string NameE { get => Value.NameE; set => Value.NameE = value; }
        public V4 Diffuse { get => Value.Diffuse; set => Value.Diffuse = value; }
        public V3 Specular { get => Value.Specular; set => Value.Specular = value; }
        public V3 Ambient { get => Value.Ambient; set => Value.Ambient = value; }
        public float Power { get => Value.Power; set => Value.Power = value; }
        public bool BothDraw { get => Value.BothDraw; set => Value.BothDraw = value; }
        public bool Shadow { get => Value.Shadow; set => Value.Shadow = value; }
        public bool SelfShadowMap { get => Value.SelfShadowMap; set => Value.SelfShadowMap = value; }
        public bool SelfShadow { get => Value.SelfShadow; set => Value.SelfShadow = value; }
        public bool VertexColor { get => Value.VertexColor; set => Value.VertexColor = value; }
        public PrimitiveType PrimitiveType { get => Value.PrimitiveType; set => Value.PrimitiveType = value; }
        public bool Edge { get => Value.Edge; set => Value.Edge = value; }
        public V4 EdgeColor { get => Value.EdgeColor; set => Value.EdgeColor = value; }
        public float EdgeSize { get => Value.EdgeSize; set => Value.EdgeSize = value; }
        public string Tex { get => Value.Tex; set => Value.Tex = value; }
        public string Sphere { get => Value.Sphere; set => Value.Sphere = value; }
        public SphereType SphereMode { get => Value.SphereMode; set => Value.SphereMode = value; }
        public string Toon { get => Value.Toon; set => Value.Toon = value; }
        public string Memo { get => Value.Memo; set => Value.Memo = value; }

        public IList<IPXFace> Faces => Value.Faces;

        public object Clone()
        {
            return Value.Clone();
        }
        #endregion
    }
}
