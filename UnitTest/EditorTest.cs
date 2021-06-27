using IwUVEditor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PEPlugin.SDX;
using System;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class EditorTest
    {
        static Editor Editor { get; set; }
        static V2[] UV => Editor.Current.Material.Vertices.Select(v => v.UV).ToArray();

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            // 具象型にして値を確定させないと呼ばれるたびにインスタンスを新規生成する
            var headVertices = PEMockFactory.CreateVertices(new V2(0, 1), new V2(-1, 0), new V2(1, -1)).ToArray();
            var tailVertices = PEMockFactory.CreateVertices(new V2(0, -2), new V2(-1.5f, -1.5f), new V2(-1.5f, 0), new V2(-2, -1), new V2(-2.5f, 0.5f)).ToArray();

            var args = PEMockFactory.CreateRunArgs(headVertices.Concat(tailVertices), (0, 1, 2), (1, 3, 2), (1, 3, 4), (5, 6, 7));

            Editor = new Editor(args, new IwUVEditor.StateContainer.EditorStates(), null);
            Editor.LoadModel();

            foreach (var key in headVertices)
            {
                Editor.Current.Material.IsSelected[key] = true;
            }
        }

        public void UndoTest()
        {
            Editor.Undo();

            Assert.AreEqual(0, UV[0].X, 1e-6);
            Assert.AreEqual(1, UV[0].Y, 1e-6);
            Assert.AreEqual(-1, UV[1].X, 1e-6);
            Assert.AreEqual(0, UV[1].Y, 1e-6);
            Assert.AreEqual(1, UV[2].X, 1e-6);
            Assert.AreEqual(-1, UV[2].Y, 1e-6);
        }

        [TestMethod]
        public void TestCopyAndPaste()
        {
            try
            {
                Editor.CopyPosition();
                Editor.PastePosition();

                Assert.AreEqual(0, UV[0].X, 1e-6);
                Assert.AreEqual(0, UV[0].Y, 1e-6);

                UndoTest();
            }
            finally
            {
                Initialize(null);
            }
        }

        [TestMethod]
        public void TestApplyEditWithValue()
        {
            try
            {
                Editor.EditParameters.MoveOffset = new SlimDX.Vector3(0, 0, 0);
                Editor.EditParameters.RotationCenter = new SlimDX.Vector3(0, 0, 0);
                Editor.EditParameters.RotationAngle = 0;
                Editor.EditParameters.ScaleCenter = new SlimDX.Vector3(0, 0, 0);
                Editor.EditParameters.ScaleRatio = new SlimDX.Vector3(1, 1, 1);

                Editor.ApplyEditWithValue();

                Assert.AreEqual(0, UV[0].X, 1e-6);
                Assert.AreEqual(1, UV[0].Y, 1e-6);
                Assert.AreEqual(-1, UV[1].X, 1e-6);
                Assert.AreEqual(0, UV[1].Y, 1e-6);
                Assert.AreEqual(1, UV[2].X, 1e-6);
                Assert.AreEqual(-1, UV[2].Y, 1e-6);

                UndoTest();
            }
            finally
            {
                Initialize(null);
            }

            try
            {
                Editor.EditParameters.MoveOffset = new SlimDX.Vector3(1, 1, 0);
                Editor.EditParameters.RotationCenter = new SlimDX.Vector3(1, 1, 0);
                Editor.EditParameters.RotationAngle = (float)(Math.PI / 2);
                Editor.EditParameters.ScaleCenter = new SlimDX.Vector3(1, 1, 0);
                Editor.EditParameters.ScaleRatio = new SlimDX.Vector3(2, 2, 1);

                Editor.ApplyEditWithValue();

                Assert.AreEqual(2, UV[0].X, 1e-6);
                Assert.AreEqual(0, UV[0].Y, 1e-6);
                Assert.AreEqual(4, UV[1].X, 1e-6);
                Assert.AreEqual(-2, UV[1].Y, 1e-6);
                Assert.AreEqual(6, UV[2].X, 1e-6);
                Assert.AreEqual(2, UV[2].Y, 1e-6);

                UndoTest();
            }
            finally
            {
                Initialize(null);
            }

            Editor.EditParameters.MoveOffset = new SlimDX.Vector3(0, 0, 0);
            Editor.EditParameters.RotationCenter = new SlimDX.Vector3(0, 0, 0);
            Editor.EditParameters.RotationAngle = 0;
            Editor.EditParameters.ScaleCenter = new SlimDX.Vector3(0, 0, 0);

            try
            {
                Editor.EditParameters.ScaleRatio = new SlimDX.Vector3(0, 1, 1);

                Editor.ApplyEditWithValue();

                Assert.AreEqual(0, UV[0].X, 1e-6);
                Assert.AreEqual(1, UV[0].Y, 1e-6);
                Assert.AreEqual(0, UV[1].X, 1e-6);
                Assert.AreEqual(0, UV[1].Y, 1e-6);
                Assert.AreEqual(0, UV[2].X, 1e-6);
                Assert.AreEqual(-1, UV[2].Y, 1e-6);

                UndoTest();
            }
            finally
            {
                Initialize(null);
            }

            try
            {
                Editor.EditParameters.ScaleRatio = new SlimDX.Vector3(1, 0, 1);

                Editor.ApplyEditWithValue();

                Assert.AreEqual(0, UV[0].X, 1e-6);
                Assert.AreEqual(0, UV[0].Y, 1e-6);
                Assert.AreEqual(-1, UV[1].X, 1e-6);
                Assert.AreEqual(0, UV[1].Y, 1e-6);
                Assert.AreEqual(1, UV[2].X, 1e-6);
                Assert.AreEqual(0, UV[2].Y, 1e-6);

                UndoTest();
            }
            finally
            {
                Initialize(null);
            }

            try
            {
                Editor.EditParameters.ScaleRatio = new SlimDX.Vector3(0, 0, 1);

                Editor.ApplyEditWithValue();

                Assert.AreEqual(0, UV[0].X, 1e-6);
                Assert.AreEqual(0, UV[0].Y, 1e-6);
                Assert.AreEqual(0, UV[1].X, 1e-6);
                Assert.AreEqual(0, UV[1].Y, 1e-6);
                Assert.AreEqual(0, UV[2].X, 1e-6);
                Assert.AreEqual(0, UV[2].Y, 1e-6);

                UndoTest();
            }
            finally
            {
                Initialize(null);
            }
        }
    }
}
