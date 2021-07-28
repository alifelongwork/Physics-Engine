using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImplementationsLibrary;
namespace VectorMatrixTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddTest()
        {
            Matrix4x4Math mat1 = Matrix4x4Math.Identity;

            Matrix4x4Math mat2 = new Matrix4x4Math(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2);

            Matrix4x4Math expect = new Matrix4x4Math(3, 2, 2, 2, 2, 3, 2, 2, 2, 2, 3, 2, 2, 2, 2, 3);

            Matrix4x4Math result = mat1 + mat2;

            Matrix4x4Math res = Matrix4x4Math.Add(mat1, mat2);

            Assert.AreEqual(res, result);
        }
    }
}
