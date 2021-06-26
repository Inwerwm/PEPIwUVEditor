using IwUVEditor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PEPlugin.SDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var args = PEMockFactory.CreateRunArgs(new[] 
            {
                new V2(0, 1), new V2(-1, 0), new V2(1, -1) 
            }, new[] 
            {
                (0, 1, 2) 
            });
            Editor = new Editor(args, new IwUVEditor.StateContainer.EditorStates(), null);
            Editor.LoadModel();

            foreach (var key in Editor.Current.Material.IsSelected.Keys.ToArray())
            {
                Editor.Current.Material.IsSelected[key] = true;
            }
        }

        public void Reset()
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
            Editor.CopyPosition();
            Editor.PastePosition();

            Assert.AreEqual(0, UV[0].X, 1e-6);
            Assert.AreEqual(0, UV[0].Y, 1e-6);

            Reset();
        }

        [TestMethod]
        public void TestApplyEditWithValue()
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

            Reset();
        }
    }
}
