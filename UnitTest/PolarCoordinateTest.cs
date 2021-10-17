using IwUVEditor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void FactoryMethodTest()
        {
            var pol = PolarCoordinate.FromPolar(0.1f, 1);
            Assert.AreEqual(0.1f, pol.Angle);
            Assert.AreEqual(1.0f, pol.Radius);

            var ord = PolarCoordinate.FromOrthogonal(3, 4);
            Assert.AreEqual(0.92729521800161f, ord.Angle);
            Assert.AreEqual(5.0f, ord.Radius);
        }

        [TestMethod]
        public void DefferenceOfAngleTest()
        {
            var orthant1 = PolarCoordinate.FromOrthogonal(1, 1);
            var orthant2 = PolarCoordinate.FromOrthogonal(-1, 1);
            var orthant3 = PolarCoordinate.FromOrthogonal(-1, -1);
            var orthant4 = PolarCoordinate.FromOrthogonal(1, -1);

            Assert.AreEqual((float)(Math.PI / 2), orthant2.DifferenceOfAngle(orthant1), 1e-6, "象限 1 -> 2");
            Assert.AreEqual((float)(Math.PI / 2), orthant3.DifferenceOfAngle(orthant2), 1e-6, "象限 2 -> 3");
            Assert.AreEqual((float)(Math.PI / 2), orthant4.DifferenceOfAngle(orthant3), 1e-6, "象限 3 -> 4");
            Assert.AreEqual((float)(Math.PI / 2), orthant1.DifferenceOfAngle(orthant4), 1e-6, "象限 4 -> 1");

            Assert.AreEqual((float)-(Math.PI / 2), orthant4.DifferenceOfAngle(orthant1), 1e-6, "象限 1 -> 4");
            Assert.AreEqual((float)-(Math.PI / 2), orthant3.DifferenceOfAngle(orthant4), 1e-6, "象限 4 -> 3");
            Assert.AreEqual((float)-(Math.PI / 2), orthant2.DifferenceOfAngle(orthant3), 1e-6, "象限 3 -> 2");
            Assert.AreEqual((float)-(Math.PI / 2), orthant1.DifferenceOfAngle(orthant2), 1e-6, "象限 2 -> 1");
        }
    }
}
