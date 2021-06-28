using IwUVEditor.ExportUV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PEPlugin.Pmx;
using PEPlugin.SDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class UVMeshTest
    {
        /*
         * コピペ用テンプレート
         var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(0, 0.1f),
                    new V2(-0.1f, 0),
                    new V2(0.1f, -0.1f),
                    new V2(0, -0.2f),
                    new V2(-0.15f, -0.15f),
                    new V2(-0.15f, 0),
                    new V2(-0.2f, -0.1f),
                    new V2(-0.25f, 0.05f),
                },
                new[] {
                    (0, 1, 2),
                    (1, 3, 2),
                    (1, 3, 4),
                    (5, 6, 7),
                });
         */

        [TestMethod]
        public void TestUVMesh_SingleRange()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(0, 0),
                    new V2(0, 1),
                    new V2(1, 0),
                    new V2(1, 1),
                },
                new[] {
                    (0, 1, 2),
                    (1, 3, 2),
                });

            var mesh = new UVMesh(model.Vertex, model.Material.First().Faces);
            Assert.AreEqual(0, mesh.MinBound.X);
            Assert.AreEqual(0, mesh.MinBound.Y);
            Assert.AreEqual(1, mesh.MaxBound.X);
            Assert.AreEqual(1, mesh.MaxBound.Y);
        }

        [TestMethod]
        public void TestUVMesh_XPositive()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(0, 0),
                    new V2(0, 1),
                    new V2(2, 0),
                    new V2(2, 1),
                },
                new[] {
                    (0, 1, 2),
                    (1, 3, 2),
                });

            var mesh = new UVMesh(model.Vertex, model.Material.First().Faces);
            Assert.AreEqual(0, mesh.MinBound.X);
            Assert.AreEqual(0, mesh.MinBound.Y);
            Assert.AreEqual(2, mesh.MaxBound.X);
            Assert.AreEqual(1, mesh.MaxBound.Y);
        }

        [TestMethod]
        public void TestUVMesh_YPositive()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(0, 0),
                    new V2(0, 2),
                    new V2(1, 0),
                    new V2(1, 2),
                },
                new[] {
                    (0, 1, 2),
                    (1, 3, 2),
                });

            var mesh = new UVMesh(model.Vertex, model.Material.First().Faces);
            Assert.AreEqual(0, mesh.MinBound.X);
            Assert.AreEqual(0, mesh.MinBound.Y);
            Assert.AreEqual(1, mesh.MaxBound.X);
            Assert.AreEqual(2, mesh.MaxBound.Y);
        }

        [TestMethod]
        public void TestUVMesh_XYPositive()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(0, 0),
                    new V2(0, 2),
                    new V2(2, 0),
                    new V2(2, 2),
                },
                new[] {
                    (0, 1, 2),
                    (1, 3, 2),
                });

            var mesh = new UVMesh(model.Vertex, model.Material.First().Faces);
            Assert.AreEqual(0, mesh.MinBound.X);
            Assert.AreEqual(0, mesh.MinBound.Y);
            Assert.AreEqual(2, mesh.MaxBound.X);
            Assert.AreEqual(2, mesh.MaxBound.Y);
        }

        [TestMethod]
        public void TestUVMesh_XNegative()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(-1, 0),
                    new V2(-1, 1),
                    new V2(1, 0),
                    new V2(1, 1),
                },
                new[] {
                    (0, 1, 2),
                    (1, 3, 2),
                });

            var mesh = new UVMesh(model.Vertex, model.Material.First().Faces);
            Assert.AreEqual(-1, mesh.MinBound.X);
            Assert.AreEqual(0, mesh.MinBound.Y);
            Assert.AreEqual(1, mesh.MaxBound.X);
            Assert.AreEqual(1, mesh.MaxBound.Y);
        }

        [TestMethod]
        public void TestUVMesh_YNegative()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(0, -1),
                    new V2(0, 1),
                    new V2(1, -1),
                    new V2(1, 1),
                },
                new[] {
                    (0, 1, 2),
                    (1, 3, 2),
                });

            var mesh = new UVMesh(model.Vertex, model.Material.First().Faces);
            Assert.AreEqual(0, mesh.MinBound.X);
            Assert.AreEqual(-1, mesh.MinBound.Y);
            Assert.AreEqual(1, mesh.MaxBound.X);
            Assert.AreEqual(1, mesh.MaxBound.Y);
        }

        [TestMethod]
        public void TestUVMesh_XYNegative()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(-1, -1),
                    new V2(-1, 1),
                    new V2(1, -1),
                    new V2(1, 1),
                },
                new[] {
                    (0, 1, 2),
                    (1, 3, 2),
                });

            var mesh = new UVMesh(model.Vertex, model.Material.First().Faces);
            Assert.AreEqual(-1, mesh.MinBound.X);
            Assert.AreEqual(-1, mesh.MinBound.Y);
            Assert.AreEqual(1, mesh.MaxBound.X);
            Assert.AreEqual(1, mesh.MaxBound.Y);
        }
    }
}
