using PEPlugin;
using PEPlugin.Pmx;
using PEPlugin.SDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    class UVMorphCreator
    {
        internal static void AddUVMorph(string morphName, int panel, IPXPmx baseModel, IPXPmx targetModel)
        {
            if (baseModel.Vertex.Count != targetModel.Vertex.Count)
                throw new InvalidOperationException("頂点数が異なるため移動量を計算できません。");

            var diff = baseModel.Vertex.Zip(targetModel.Vertex, (b, t) => (Vertex: t, Offset: t.UV - b.UV));
            var morph = CreateUVMorph(morphName, panel, diff);
            targetModel.Morph.Add(morph);
        }

        private static IPXMorph CreateUVMorph(string morphName, int panel, IEnumerable<(IPXVertex Vertex, V2 Offset)> diff)
        {
            var morph = PEStaticBuilder.Pmx.Morph();
            morph.Kind = MorphKind.UV;
            morph.Name = morphName;
            morph.Panel = panel;
            foreach (var d in diff)
            {
                morph.Offsets.Add(PEStaticBuilder.Pmx.UVMorphOffset(d.Vertex, new PEPlugin.SDX.V4(d.Offset.X, d.Offset.Y, 0, 0)));
            }

            return morph;
        }
    }
}
