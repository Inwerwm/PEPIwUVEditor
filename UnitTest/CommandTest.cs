using IwUVEditor.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PEPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.PMX;

namespace UnitTest
{
    [TestClass]
    public class CommandTest
    {
        private IEnumerable<T> CreateCollection<T>(Func<T> constructor) {
            while (true) yield return constructor();
        }

        private IEnumerable<T> CreateCollection<T>(int count, Func<T> constructor) =>
            CreateCollection(constructor).Take(count);

        [TestMethod]
        public void ReverseTest()
        {
            var vertices = CreateCollection(3, () => new MockVertex()).ToArray();

            vertices[0].UV = new PEPlugin.SDX.V2(0.3f, 1);
            vertices[1].UV = new PEPlugin.SDX.V2(-0.3f, 1);
            vertices[2].UV = new PEPlugin.SDX.V2(0, -1);

            // 垂直反転
            new CommandReverse(vertices, CommandReverse.Axis.X).Do();

            Assert.AreEqual(-0.3f, vertices[0].UV.X);
            Assert.AreEqual(0.3f, vertices[1].UV.X);

            // 水平反転
            new CommandReverse(vertices, CommandReverse.Axis.Y).Do();

            Assert.AreEqual(1, vertices[2].UV.Y);
        }
    }
}
