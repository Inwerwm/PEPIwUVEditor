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

        public static IPERunArgs CreateRunArgs(IPXPmx model)
        {
            var pmxCon = new MockPmxConnector(model);
            var con = new MockConnector(pmxCon);
            var host = new MockPluginHost(con);
            return new MockRunArgs(host);
        }

        public static IPXPmx CreateModel(V2[] uvs, (int A, int B, int C)[] indices)
        {
            var vertices = uvs.Select(v => (IPXVertex)new MockVertex(v)).ToList();
            var material = new[] { new MockMaterial(vertices, indices) };
            return new MockPmx(vertices, material);
        }
    }
}
