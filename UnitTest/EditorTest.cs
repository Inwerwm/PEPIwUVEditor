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

        public void IsInitSuccess()
        {
            Assert.AreEqual(0, UV[0].X);
            Assert.AreEqual(1, UV[0].Y);
            Assert.AreEqual(-1, UV[1].X);
            Assert.AreEqual(0, UV[1].Y);
            Assert.AreEqual(1, UV[2].X);
            Assert.AreEqual(-1, UV[2].Y);
        }

        [TestMethod]
        public void TestCopyAndPaste()
        {
            Editor.CopyPosition();
            Editor.PastePosition();

            Assert.AreEqual(0, UV[0].X);
            Assert.AreEqual(0, UV[0].Y);

            Editor.Undo();
            IsInitSuccess();
        }
    }
}
