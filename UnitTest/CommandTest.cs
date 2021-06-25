using IwUVEditor.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PEPlugin.Pmx;
using PEPlugin.SDX;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTest.PMX;

namespace UnitTest
{
    [TestClass]
    public class CommandTest
    {
        private IEnumerable<T> CreateCollection<T>(Func<T> constructor)
        {
            while (true) yield return constructor();
        }

        private IEnumerable<T> CreateCollection<T>(int count, Func<T> constructor) =>
            CreateCollection(constructor).Take(count);

        [TestMethod]
        public void TestReverse()
        {
            var vertices = CreateCollection(3, () => new MockVertex()).ToArray();

            vertices[0].UV = new V2(0.3f, 1);
            vertices[1].UV = new V2(-0.3f, 1);
            vertices[2].UV = new V2(0, -1);

            // 垂直反転
            var vrCmd = new CommandReverse(vertices, CommandReverse.Axis.X);

            vrCmd.Do();
            Assert.AreEqual(-0.3f, vertices[0].UV.X);
            Assert.AreEqual(0.3f, vertices[1].UV.X);

            vrCmd.Undo();
            Assert.AreEqual(0.3f, vertices[0].UV.X);
            Assert.AreEqual(-0.3f, vertices[1].UV.X);

            // 水平反転
            var hrCmd = new CommandReverse(vertices, CommandReverse.Axis.Y);

            hrCmd.Do();
            Assert.AreEqual(1, vertices[2].UV.Y);

            hrCmd.Undo();
            Assert.AreEqual(-1, vertices[2].UV.Y);
        }

        [TestMethod]
        public void TestVertexEdit()
        {
            var vertices = new List<IPXVertex>
            {
                new MockVertex(new V2(0, 0)),
                new MockVertex(new V2(0, 1)),
                new MockVertex(new V2(1, 0)),
                new MockVertex(new V2(1, 1)),
            };

            var transCmd = new CommandApplyVertexEdit(vertices, Matrix.Translation(new Vector3(1, 1, 0)));
            var rotateCmd = new CommandApplyVertexEdit(vertices, Matrix.RotationZ((float)(Math.PI / 2)));
            var scaleCmd = new CommandApplyVertexEdit(vertices, Matrix.Scaling(new Vector3(2, 2, 1)));
            
            transCmd.Do();
            Assert.AreEqual(1, vertices[0].UV.X, 1e-6);
            Assert.AreEqual(1, vertices[0].UV.Y, 1e-6);
            Assert.AreEqual(2, vertices[3].UV.X, 1e-6);
            Assert.AreEqual(2, vertices[3].UV.Y, 1e-6);

            transCmd.Undo();
            Assert.AreEqual(0, vertices[0].UV.X, 1e-6);
            Assert.AreEqual(0, vertices[0].UV.Y, 1e-6);
            Assert.AreEqual(1, vertices[3].UV.X, 1e-6);
            Assert.AreEqual(1, vertices[3].UV.Y, 1e-6);

            rotateCmd.Do();
            Assert.AreEqual(0, vertices[2].UV.X, 1e-6);
            Assert.AreEqual(1, vertices[2].UV.Y, 1e-6);

            rotateCmd.Undo();
            Assert.AreEqual(1, vertices[2].UV.X, 1e-6);
            Assert.AreEqual(0, vertices[2].UV.Y, 1e-6);

            scaleCmd.Do();
            Assert.AreEqual(2, vertices[3].UV.X, 1e-6);
            Assert.AreEqual(2, vertices[3].UV.Y, 1e-6);

            scaleCmd.Undo();
            Assert.AreEqual(1, vertices[3].UV.X, 1e-6);
            Assert.AreEqual(1, vertices[3].UV.Y, 1e-6);
        }
    }
}
