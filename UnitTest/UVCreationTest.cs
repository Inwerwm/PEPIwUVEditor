using IwUVEditor;
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
    public class UVCreationTest
    {
        [TestMethod]
        public void TestUVCreation()
        {
            var baseVertices = PEMockFactory.CreateVertices(new[]
                {
                    new V2(0.0f, 0.0f),
                    new V2(0.1f, 0.1f),
                    new V2(0.2f, 0.2f),
                }).ToList();
            IPXPmx baseModel = PEMockFactory.CreateModel(baseVertices, new[] { (0, 1, 2) });

            var targetVertices = PEMockFactory.CreateVertices(new[]
                {
                    new V2(0.0f, 0.0f),
                    new V2(0.2f, 0.3f),
                    new V2(0.2f, 0.2f),
                }).ToList();
            IPXPmx targetModel = PEMockFactory.CreateModel(targetVertices, new[] { (0, 1, 2) });

            const string morphName = "created";
            UVMorphCreator.AddUVMorph(morphName, 4, baseModel, targetModel);

            var m = targetModel.Morph.First();
            var v = targetVertices.ElementAt(2);
            var o = ((IPXUVMorphOffset)m.Offsets.First());
            
            Assert.AreEqual(morphName, m.Name);
            Assert.AreEqual(1, m.Offsets.Count);
            Assert.AreEqual(v, o.Vertex);
            Assert.AreEqual(0.1f, o.Offset.X);
            Assert.AreEqual(0.3f, o.Offset.Y);
        }
    }
}
