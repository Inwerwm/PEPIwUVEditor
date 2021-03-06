﻿using IwUVEditor;
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
    public class UVMorphEditorTest
    {
        [TestMethod]
        public void TestCreateUVMorph()
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

            UVMorphEditor.Builder = PEMockFactory.Builder;
            const string morphName = "created";
            UVMorphEditor.AddUVMorph(morphName, 4, baseModel, targetModel);

            var m = baseModel.Morph.First();
            var v = baseVertices.ElementAt(1);
            var o = ((IPXUVMorphOffset)m.Offsets.First());
            
            Assert.AreEqual(morphName, m.Name);
            Assert.AreEqual(1, m.Offsets.Count);
            Assert.AreEqual(v, o.Vertex);
            Assert.AreEqual(0.1f, o.Offset.X, 1e-6);
            Assert.AreEqual(0.2f, o.Offset.Y, 1e-6);
        }
    }
}
