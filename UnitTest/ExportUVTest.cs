using IwUVEditor.ExportUV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PEPlugin.SDX;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class ExportUVTest
    {
        IUVDrawer Drawer { get; } = new GDIUVDrawer();

        [TestMethod]
        public void TestUVMesh_SingleRange()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(0.1f, 0.1f),
                    new V2(0.1f, 0.9f),
                    new V2(0.9f, 0.1f),
                    new V2(0.9f, 0.9f),
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

            var repeatCount = Drawer.CalcTextureRepeatCount(mesh);
            Assert.AreEqual(1, repeatCount.X);
            Assert.AreEqual(1, repeatCount.Y);
        }

        [TestMethod]
        public void TestUVMesh_XPositive()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(0.3f, 0.3f),
                    new V2(0.3f, 0.9f),
                    new V2(1.9f, 0.3f),
                    new V2(1.9f, 0.9f),
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

            var repeatCount = Drawer.CalcTextureRepeatCount(mesh);
            Assert.AreEqual(2, repeatCount.X);
            Assert.AreEqual(1, repeatCount.Y);
        }

        [TestMethod]
        public void TestUVMesh_YPositive()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(0.3f, 0.3f),
                    new V2(0.3f, 1.9f),
                    new V2(0.9f, 0.3f),
                    new V2(0.9f, 1.9f),
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

            var repeatCount = Drawer.CalcTextureRepeatCount(mesh);
            Assert.AreEqual(1, repeatCount.X);
            Assert.AreEqual(2, repeatCount.Y);
        }

        [TestMethod]
        public void TestUVMesh_XYPositive()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(0.3f, 0.3f),
                    new V2(0.3f, 1.9f),
                    new V2(1.9f, 0.3f),
                    new V2(1.9f, 1.9f),
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

            var repeatCount = Drawer.CalcTextureRepeatCount(mesh);
            Assert.AreEqual(2, repeatCount.X);
            Assert.AreEqual(2, repeatCount.Y);
        }

        [TestMethod]
        public void TestUVMesh_XNegative()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(-0.9f, 0.3f),
                    new V2(-0.9f, 0.9f),
                    new V2(0.9f, 0.3f),
                    new V2(0.9f, 0.9f),
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

            var repeatCount = Drawer.CalcTextureRepeatCount(mesh);
            Assert.AreEqual(2, repeatCount.X);
            Assert.AreEqual(1, repeatCount.Y);
        }

        [TestMethod]
        public void TestUVMesh_YNegative()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(0.3f, -0.9f),
                    new V2(0.3f, 0.9f),
                    new V2(0.9f, -0.9f),
                    new V2(0.9f, 0.9f),
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

            var repeatCount = Drawer.CalcTextureRepeatCount(mesh);
            Assert.AreEqual(1, repeatCount.X);
            Assert.AreEqual(2, repeatCount.Y);
        }

        [TestMethod]
        public void TestUVMesh_XYNegative()
        {
            var model = PEMockFactory.CreateModel(
                new[] {
                    new V2(-0.9f, -0.9f),
                    new V2(-0.9f, 0.9f),
                    new V2(0.9f, -0.9f),
                    new V2(0.9f, 0.9f),
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

            var repeatCount = Drawer.CalcTextureRepeatCount(mesh);
            Assert.AreEqual(2, repeatCount.X);
            Assert.AreEqual(2, repeatCount.Y);
        }
    }
}
