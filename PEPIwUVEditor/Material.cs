﻿using PEPExtensions;
using PEPlugin.Pmx;
using PEPlugin.SDX;
using SlimDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IwUVEditor
{
    delegate void MaterialTextureChangedEventHandler(string fullPath);

    class Material : IPXMaterial
    {
        private static int instanceCount;
        public int InstanceId { get; }

        public event MaterialTextureChangedEventHandler MaterialTextrueChanged;

        IPXMaterial Value { get; }

        public IList<IPXVertex> Vertices { get; }
        public Dictionary<IPXVertex, bool> IsSelected { get; }
        public Matrix TemporaryTransformMatrices { get; set; }

        /// <summary>
        /// インデックスバッファに与える面の構成頂点番号配列
        /// </summary>
        public uint[] FaceSequence { get; }

        public string ModelPath { get; }
        public string TexFullPath { get; private set; }

        public string TexExt => Path.GetExtension(Tex);

        public Material(IPXMaterial material, IPXPmx pmx)
        {
            InstanceId = instanceCount;
            instanceCount++;

            Value = material;
            ModelPath = pmx.FilePath;
            CreateTexFullPath();

            IEnumerable<IPXVertex> faceVertices = Faces.SelectMany(face => face.ToVertices());

            Vertices = faceVertices.Distinct().ToList();
            IsSelected = Vertices.ToDictionary(vtx => vtx, _ => false);
            TemporaryTransformMatrices = Matrix.Identity;

            var VtxIdDic = Vertices.Select((vtx, i) => (vtx, i)).ToDictionary(pair => pair.vtx, pair => (uint)pair.i);
            FaceSequence = faceVertices.Select(vtx => VtxIdDic[vtx]).ToArray();
        }

        private void CreateTexFullPath()
        {
            if (string.IsNullOrWhiteSpace(ModelPath) || string.IsNullOrWhiteSpace(Tex))
            {
                TexFullPath = "";
                return;
            }

            // カレントディレクトリをモデルのディレクトリに変更
            var cdTmp = Environment.CurrentDirectory;
            Environment.CurrentDirectory = Path.GetDirectoryName(ModelPath);

            // カレントディレクトリを基準にテクスチャの絶対パスを取得
            string fullPath = Path.GetFullPath(Tex);

            // カレントディレクトリをもとに戻す
            Environment.CurrentDirectory = cdTmp;

            // ファイルが存在しなければ空文字を返す
            TexFullPath = File.Exists(fullPath) ? fullPath : "";
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
        public string Tex
        {
            get => Value.Tex;
            set
            {
                Value.Tex = value;
                CreateTexFullPath();
                MaterialTextrueChanged?.Invoke(TexFullPath);
            }
        }
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
