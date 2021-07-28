using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhysicsLibrary;
namespace VectorMatrixTests
{
    [TestClass]
    public class UnitClassTest
    {
        [TestMethod]
        public void ConstructionTest()
        {
            Unit<float> unit = new Unit<float>(10, 1);

            Assert.IsNotNull(unit);
        }

        [TestMethod]
        public void AdditionTest()
        {
            Unit<float> unit = new Unit<float>(10, 1);
            unit += 10;
            unit += new Unit<float>(10, 1);

            Assert.IsTrue(unit.Value == 30f);
        }
        [TestMethod]
        public void SubtractionTest()
        {
            Unit<float> unit = new Unit<float>(30, 1);
            unit -= 10;
            unit -= new Unit<float>(10, 1);

            Assert.IsTrue(unit.Value == 10f);
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            Unit<float> unit = new Unit<float>(2, 1);
            unit *= 3;
            unit *= new Unit<float>(4, 1);

            Assert.IsTrue(unit.Value == 24f);
        }

        [TestMethod]
        public void DivisionTest()
        {
            Unit<float> unit = new Unit<float>(24, 1);
            unit /= 3;
            unit /= new Unit<float>(4, 1);

            Assert.IsTrue(unit.Value == 2f);
        }


        [TestMethod]
        public void ConversionTest()
        {
            Unit<float> unit = new Unit<float>(24, false, 2f);
            
            Assert.IsTrue(unit.PixelValue == 48f);
            Assert.IsTrue(unit.SIValue == 24f);
            Assert.IsTrue(unit.Value == 24f);
            unit.SwitchToOther();
            Assert.IsTrue(unit.PixelValue == 48f);
            Assert.IsTrue(unit.SIValue == 24f);
            Assert.IsTrue(unit.Value == 48f);
            unit.SwitchToOther();
            Assert.IsTrue(unit.PixelValue == 48f);
            Assert.IsTrue(unit.SIValue == 24f);
            Assert.IsTrue(unit.Value == 24f);
        }
    }
}
