using PEPlugin;
using PEPlugin.Pmx;
using PEPlugin.SDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.PEMock;

namespace UnitTest
{
    static class PEMockFactory
    {
        public static IPERunArgs CreateRunArgs(V2[] uvs, (int A, int B, int C)[] indices) =>
            CreateRunArgs(CreateModel(uvs, indices));

        public static IPERunArgs CreateRunArgs(IEnumerable<IPXVertex> vertices, params (int A, int B, int C)[] indices) =>
            CreateRunArgs(CreateModel(vertices.ToList(), indices));

        public static IPERunArgs CreateRunArgs(IPXPmx model)
        {
            var pmxCon = new MockPmxConnector(model);
            var con = new MockConnector(pmxCon);
            var host = new MockPluginHost(con);
            return new MockRunArgs(host);
        }

        public static IPXPmx CreateModel(V2[] uvs, (int A, int B, int C)[] indices)
        {
            return CreateModel(CreateVertices(uvs).ToList(), indices);
        }

        public static IPXPmx CreateModel(IList<IPXVertex> vertices, (int A, int B, int C)[] indices)
        {
            var material = new[] { new MockMaterial(vertices, indices) };
            return new MockPmx(vertices, material);
        }
        public static IEnumerable<IPXVertex> CreateVertices(params V2[] uvs)
        {
            return uvs.Select(v => (IPXVertex)new MockVertex(v));
        }
    }
}
