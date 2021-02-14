using DxManager.Camera;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlimDX;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {

            float cPos = 0;
            float zoom = 100;
            
            int width = 1009;
            int height = 911;

            var camera = new DxCameraOrthographic()
            {
                Position = new SlimDX.Vector3(cPos, cPos, -1),
                Target = new SlimDX.Vector3(cPos, cPos, 0),
                Up = new SlimDX.Vector3(0, -1, 0),
                ViewVolumeSize = (width / zoom, height / zoom),
                ViewVolumeDepth = (0, 1)
            };
            Matrix transMatrix = camera.GetMatrix();

            Vector4 value = new Vector4(0, 0, 0, 1);

            Matrix vprtMat = Matrix.Identity;
            vprtMat.M11 = width / 2;
            vprtMat.M14 = width / 2;
            vprtMat.M22 = -height / 2;
            vprtMat.M24 = height / 2;

            Vector4 expectPos = Vector4.Transform(value, transMatrix * vprtMat);

            Vector2 normalizedPos = new Vector2(value.X / width, value.Y / height);


            Matrix iViewMat = Matrix.Invert(camera.CreateViewMatrix());
            Matrix iPrjMat = Matrix.Invert((camera as DxManager.Camera.DxCameraOrthographic).CreateProjectionMatrix());
            Matrix iVprtMat = Matrix.Invert(vprtMat);
            Vector4 worldPos = Vector4.Transform(new Vector4(normalizedPos, 0, 1), Matrix.Invert(transMatrix));

            var mousePos = new Vector2(worldPos.X, worldPos.Y);
        }
    }
}
