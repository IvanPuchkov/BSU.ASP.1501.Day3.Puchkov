using NUnit.Framework;

namespace Polynomial.Tests
{
    [TestFixture]
    public class PolynomialTests
    {
        [TestCase(new double[] {2,5,4}, Result="2*a^2 + 5*a + 4")]
        [TestCase(new double[] { 4,3 }, Result = "4*a + 3")]
        public string ToString_Tests(double[] array)
        {
            return new Polynomial(array).ToString();
        }

        
        [TestCase(new double[] { 4, 3 }, Result =false)]
        [TestCase(new double[] { 1, 2, 3, 4, 5 }, Result = true)]
        public bool Equals_Tests(double[] array)
        {
            Polynomial polynomial=new Polynomial(array);
            Polynomial anotherPolynomial =new Polynomial(new double[] {1,2,3,4,5});
            return anotherPolynomial.Equals(polynomial);
        }

        [TestCase(new double[] { 4, 3 }, new double[] {1,2,3,8,8})]
        [TestCase(new double[] { 1, 2, 3, 4, 5 }, new double[] { 2, 4, 6, 8, 10 })]
        public void Add_Tests(double[] array,double[] resultArray)
        {
            Polynomial polynomial = new Polynomial(array);
            Polynomial anotherPolynomial = new Polynomial(new double[] { 1, 2, 3, 4, 5 });
            Assert.AreEqual(resultArray, (polynomial + anotherPolynomial).ToArray());
        }

        [TestCase(new double[] { 1, 2, 3, 4, 5 }, new double[] { 0, 0, 0, 0, 0 })]
        [TestCase(new double[] { 4, 3 }, new double[] { -1, -2, -3, 0, -2 })]
        public void Substract_Tests(double[] array, double[] resultArray)
        {
            Polynomial polynomial = new Polynomial(array);
            Polynomial anotherPolynomial = new Polynomial(new double[] { 1, 2, 3, 4, 5 });
            Assert.AreEqual(resultArray, (polynomial - anotherPolynomial).ToArray());
        }

        [TestCase(new double[] { 2,5,4 }, new double[] { 4,3 },new double[] {8,26,31,12})]
        [TestCase(new double[] { 1, 2, 3 }, new double[] { 4, 5, 6 }, new double[] { 4, 13, 28, 27, 18 })]
        public void Multiply_Tests(double[] firstArray,double[] secondArray, double[] resultArray)
        {
            Polynomial a = new Polynomial(firstArray);
            Polynomial b = new Polynomial(secondArray);
            Assert.AreEqual(resultArray, (a * b).ToArray());
        }
    }
}
