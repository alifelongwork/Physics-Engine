using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImplementationsLibrary;

namespace VectorMatrixTests
{
    [TestClass]
    public class Matrix3x3ArrayTests
    {
        [TestMethod]
        public void CreateTranslation()
        {
            Matrix3x3MathArray initial = Matrix3x3MathArray.CreateTranslation(20, 20);

            Assert.AreEqual(initial.X, 20);
            Assert.AreEqual(initial.Y, 20);

            initial *= Matrix3x3MathArray.CreateTranslation(25, 35);

            Assert.AreEqual(initial.X, 45);
            Assert.AreEqual(initial.Y, 55);
        }

        [TestMethod]
        public void CreateRotationZ()
        {
            Matrix3x3MathArray initial = Matrix3x3MathArray.CreateTranslation(10, 0);

            //Assert.AreEqual(initial.Theta, (float)Math.PI/2);

            initial *= Matrix3x3MathArray.CreateRotationZ((float)Math.PI / 2);

            Assert.AreEqual(Math.Round(initial.X, 1), 0);
            Assert.AreEqual(initial.Y, 10);

            initial *= Matrix3x3MathArray.CreateRotationZ(-(float)Math.PI / 2);

            Assert.AreEqual(initial.X, 10);
            Assert.AreEqual(Math.Round(initial.Y, 1), 0);
        }


        [TestMethod]
        public void CreateRotationZAroundPoint()
        {
            Matrix3x3MathArray initial = Matrix3x3MathArray.CreateTranslation(0, 10);

            initial *= Matrix3x3MathArray.CreateRotationZ((float)Math.PI / 2, 10, 10);

            Assert.AreEqual(initial.X, 10);
            Assert.AreEqual(Math.Round(initial.Y, 1), 0);

            initial *= Matrix3x3MathArray.CreateRotationZ(-(float)Math.PI / 2, 10, 10);

            Assert.AreEqual(Math.Round(initial.X, 1), 0);
            Assert.AreEqual(initial.Y, 10);
        }


        [TestMethod]
        public void CreateRotationZMultiple()
        {
            int amountOfSteps = 16;
            for (int i = 0; i < amountOfSteps; i++)
            {
                float rotation = i * 2 * (float)Math.PI / amountOfSteps;
                Matrix3x3MathArray matrix = Matrix3x3MathArray.CreateRotationZ(rotation, 50, 50);
                //Assert.AreEqual(Math.Round(rotation, 5), Math.Round((float)Math.PI * 2 - matrix.Theta, 5));
            }
        }
    }
}
