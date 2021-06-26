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
        Editor Editor { get; set; }

        [ClassInitialize]
        public void Initialize()
        {
            var args = PEMockFactory.CreateRunArgs(new[] { new V2(0, 1), new V2(-1, -1), new V2(1, -1) }, new[] { (0, 1, 2) });
            Editor = new Editor(args, new IwUVEditor.StateContainer.EditorStates(), null);
            Editor.LoadModel();
        }


    }
}
