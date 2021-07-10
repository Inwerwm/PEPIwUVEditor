using IwUVEditor.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PEPlugin.Pmx;
using PEPlugin.SDX;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTest.PEMock;

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
            var vertices = new[]
            {
                new MockVertex(new V2(0, 0)),
                new MockVertex(new V2(0, 1)),
                new MockVertex(new V2(1, 0)),
                new MockVertex(new V2(1, 1)),
            };

            Matrix transMat = Matrix.Translation(new Vector3(1, 1, 0));
            Matrix rotMat = Matrix.RotationZ((float)(Math.PI / 2));
            Matrix scaleMat = Matrix.Scaling(new Vector3(2, 2, 1));

            // 平行移動
            var transCmd = new CommandApplyVertexEdit(vertices, transMat);
            
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

            // 回転
            var rotateCmd = new CommandApplyVertexEdit(vertices, rotMat);

            rotateCmd.Do();
            Assert.AreEqual(0, vertices[2].UV.X, 1e-6);
            Assert.AreEqual(1, vertices[2].UV.Y, 1e-6);

            rotateCmd.Undo();
            Assert.AreEqual(1, vertices[2].UV.X, 1e-6);
            Assert.AreEqual(0, vertices[2].UV.Y, 1e-6);

            // 拡大
            var scaleCmd = new CommandApplyVertexEdit(vertices, scaleMat);

            scaleCmd.Do();
            Assert.AreEqual(2, vertices[3].UV.X, 1e-6);
            Assert.AreEqual(2, vertices[3].UV.Y, 1e-6);

            scaleCmd.Undo();
            Assert.AreEqual(1, vertices[3].UV.X, 1e-6);
            Assert.AreEqual(1, vertices[3].UV.Y, 1e-6);

            // 複合
            var compMat = scaleMat * rotMat * transMat;
            var compCmd = new CommandApplyVertexEdit(vertices, compMat);

            compCmd.Do();
            Assert.AreEqual(1, vertices[0].UV.X, 1e-6);
            Assert.AreEqual(1, vertices[0].UV.Y, 1e-6);
            Assert.AreEqual(-1, vertices[1].UV.X, 1e-6);
            Assert.AreEqual(1, vertices[1].UV.Y, 1e-6);
            Assert.AreEqual(1, vertices[2].UV.X, 1e-6);
            Assert.AreEqual(3, vertices[2].UV.Y, 1e-6);
            Assert.AreEqual(-1, vertices[3].UV.X, 1e-6);
            Assert.AreEqual(3, vertices[3].UV.Y, 1e-6);

            compCmd.Undo();
            Assert.AreEqual(0, vertices[0].UV.X, 1e-6);
            Assert.AreEqual(0, vertices[0].UV.Y, 1e-6);
            Assert.AreEqual(0, vertices[1].UV.X, 1e-6);
            Assert.AreEqual(1, vertices[1].UV.Y, 1e-6);
            Assert.AreEqual(1, vertices[2].UV.X, 1e-6);
            Assert.AreEqual(0, vertices[2].UV.Y, 1e-6);
            Assert.AreEqual(1, vertices[3].UV.X, 1e-6);
            Assert.AreEqual(1, vertices[3].UV.Y, 1e-6);
        }

        [TestMethod]
        public void TestMoveByMorph()
        {
            var vertices = PEMockFactory.CreateVertices(new V2(0, 0), new V2(1, 0), new V2(0, 1), new V2(1, 1)).ToArray();
            var offsets = vertices.Select(v => PEMockFactory.Builder.UVMorphOffset(v, new V4(1, 1, 0, 0)));

            var cmd = new CommandMoveVerticesByMorph(offsets);

            cmd.Do();
            Assert.AreEqual(1, vertices[0].UV.X);
            Assert.AreEqual(1, vertices[0].UV.Y);
            Assert.AreEqual(2, vertices[1].UV.X);
            Assert.AreEqual(1, vertices[1].UV.Y);
            Assert.AreEqual(1, vertices[2].UV.X);
            Assert.AreEqual(2, vertices[2].UV.Y);
            Assert.AreEqual(2, vertices[3].UV.X);
            Assert.AreEqual(2, vertices[3].UV.Y);

            cmd.Undo();
            Assert.AreEqual(0, vertices[0].UV.X);
            Assert.AreEqual(0, vertices[0].UV.Y);
            Assert.AreEqual(1, vertices[1].UV.X);
            Assert.AreEqual(0, vertices[1].UV.Y);
            Assert.AreEqual(0, vertices[2].UV.X);
            Assert.AreEqual(1, vertices[2].UV.Y);
            Assert.AreEqual(1, vertices[3].UV.X);
            Assert.AreEqual(1, vertices[3].UV.Y);
        }

        [TestMethod]
        public void TestTransaction()
        {
            var vertices = PEMockFactory.CreateVertices(new V2(0, 0), new V2(1, 0), new V2(0, 1), new V2(1, 1)).ToArray();

            var transaction = new CommandTransaction(new[]
            {
                new CommandApplyVertexEdit(vertices, Matrix.Translation(1, 0, 0)),
                new CommandApplyVertexEdit(vertices, Matrix.Translation(0, 1, 0)),
                new CommandApplyVertexEdit(vertices, Matrix.Translation(1, 1, 0))
            });

            transaction.Do();
            Assert.AreEqual(2, vertices[0].UV.X);
            Assert.AreEqual(2, vertices[0].UV.Y);
            Assert.AreEqual(3, vertices[1].UV.X);
            Assert.AreEqual(2, vertices[1].UV.Y);
            Assert.AreEqual(2, vertices[2].UV.X);
            Assert.AreEqual(3, vertices[2].UV.Y);
            Assert.AreEqual(3, vertices[3].UV.X);
            Assert.AreEqual(3, vertices[3].UV.Y);

            transaction.Undo();
            Assert.AreEqual(0, vertices[0].UV.X);
            Assert.AreEqual(0, vertices[0].UV.Y);
            Assert.AreEqual(1, vertices[1].UV.X);
            Assert.AreEqual(0, vertices[1].UV.Y);
            Assert.AreEqual(0, vertices[2].UV.X);
            Assert.AreEqual(1, vertices[2].UV.Y);
            Assert.AreEqual(1, vertices[3].UV.X);
            Assert.AreEqual(1, vertices[3].UV.Y);
        }
    }
}
