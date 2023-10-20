using System;
using Triangle;


namespace TriangleTests
{
    [TestFixture]
    public class TriangleInfoTests
    {
       

        

        [Test]
        public void GetTriangleInfo_NotTriangle_ReturnsCorrectResult()
        {
            // Arrange
            string sideA = "1";
            string sideB = "2";
            string sideC = "3";

            // Act
            var result = Triangle1.GetTriangleInfo(sideA, sideB, sideC);

            // Assert
            Assert.AreEqual("не треугольник", result.Item1);
            Assert.AreEqual(new List<(int, int)> { (-4, -1), (-1, -1), (-1, -1) }, result.Item2);
        }

        [Test]
        public void GetTriangleType_NegativeSide_ReturnsEmptyString()
        {

            // Arrange
            var GetTriangleType = new Triangle1();
            float sideA = -2;
            float sideB = 4;
            float sideC = 5;

            // Act
            var result = Triangle1.GetTirangleType(sideA, sideB, sideC);

            // Assert
            Assert.AreEqual("", result);
        }

        [Test]
        public void GetTriangleType_NotTriangle_ReturnsNotTriangle()
        {
            // Arrange
            float sideA = 1;
            float sideB = 2;
            float sideC = 10;

            // Act
            var result = Triangle1.GetTirangleType(sideA, sideB, sideC);

            // Assert
            Assert.AreEqual("не треугольник", result);
        }

        

        
    }
}