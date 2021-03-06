﻿using PEPlugin;
using PEPlugin.Pmx;
using PEPlugin.SDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    class UVMorphEditor
    {
        public static float Delta { get; set; } = 1e-6f;
        public static IPXPmxBuilder Builder { get; set; } = PEStaticBuilder.Pmx;

        internal static void AddUVMorph(string morphName, int panel, IPXPmx baseModel, IPXPmx targetModel)
        {
            if (baseModel.Vertex.Count != targetModel.Vertex.Count)
                throw new InvalidOperationException("頂点数が異なるため移動量を計算できません。");

            var diff = baseModel.Vertex.Zip(targetModel.Vertex, (b, t) => (Vertex: b, Offset: t.UV - b.UV)).Where(d => d.Offset.X > Delta || d.Offset.Y > Delta);
            var morph = CreateUVMorph(morphName, panel, diff);
            baseModel.Morph.Add(morph);
        }

        private static IPXMorph CreateUVMorph(string morphName, int panel, IEnumerable<(IPXVertex Vertex, V2 Offset)> diff)
        {
            var morph = Builder.Morph();
            morph.Kind = MorphKind.UV;
            morph.Name = morphName;
            morph.Panel = panel;
            foreach (var d in diff)
            {
                morph.Offsets.Add(Builder.UVMorphOffset(d.Vertex, new V4(d.Offset.X, d.Offset.Y, 0, 0)));
            }

            return morph;
        }
    }
}
