using DxManager.Camera;
using IwUVEditor.DirectX.DrawElement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlimDX;
using System;

namespace UnitTest
{
    [TestClass]
    public class StructureTest
    {

        [TestMethod]
        public void TriangleTest()
        {
            Triangle t = new Triangle(
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(0.5f, 1, 0)
            );

            Assert.IsTrue(t.Include(new Vector3(0.5f, 0.5f, 0)));
            Assert.IsFalse(t.Include(new Vector3(0, 1, 0)));
        }
    }
}
