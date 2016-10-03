using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp7
{
    [TestClass]
    public class CSharp7Tests
    {
        // Confirm C# 7.0 Is Active using a Binary literal
        int BinaryNumber { get; } = 10_10_10;

        (int Height, int Width, int Length) DefaultCubeSize = (1, 2, 3);


        public long DivideWithRemainder(
            long numerator, long denominator, out long remainder)
        {
            remainder = numerator % denominator;
            return (numerator / denominator);
        }


        [TestMethod]
        public void DivideTest()
        {
            Assert.AreEqual<long>(21, DivideWithRemainder(42, 2, out long remainder));
            Assert.AreEqual<long>(0, remainder);

            // ERROR: A local variable named 'remainder' is already defined in the scope.
            // Assert.AreEqual<decimal>(21, Divide(42, 2, out decimal remainder));  

            void assertDivide(
                long expected, long expectedRemainder, 
                long numerator, long denomenator)
            {
                Assert.AreEqual<long>(
                    expected, DivideWithRemainder(numerator, denomenator, out long remaining));
                Assert.AreEqual<decimal>(expectedRemainder, remaining);
            }
            assertDivide(21, 0, 42, 2);
            assertDivide(21, 1, 43, 2);


        }

        [TestMethod]
        public void ReturnTupleWithNamedProperties()
        {

            (double Latitude, double Longitude) GetCurrentCoordinate(string address = null)
            {
                if (address is null)
                {
                    //  "10 Downing Street, London, United Kingdom"
                    return (51.50336335364312, -0.12762486934661865);
                }
                else
                {
                    // Lookup look up current location 
                    // ...
                    return (16.766123, -3.004031); // Assumes Timbuktu
                }
            }

            (double Latitude, double Longitude) expected =
                (51.50336335364312, -0.12762486934661865);

            (double Latitude, double Longitude) actual = 
                GetCurrentCoordinate("10 Downing Street, London, United Kingdom");
            Assert.AreEqual<(double Latitude, double Longitude)>(
                expected, actual);
            Assert.AreEqual<double>(51.50336335364312, actual.Latitude);
            Assert.AreEqual<double>(51.50336335364312, actual.Latitude);

            // WARNING: The tuple element name 'Lat' is ignored because
            // a different name is specified by the target type
            // '(double Latitude, double Longitude).
            expected = (Lat: 16.766123, Long: -3.004031);

            Assert.AreEqual<(double Latitude, double Longitude)>(
                expected, GetCurrentCoordinate());
            
            Assert.AreEqual<(double Latitude, double Longitude)>(
                (Lat: 16.766123, Long: -3.004031), GetCurrentCoordinate());

            
            //(double, double) GetSampleCoordinate() => (Lat: 16.766123, Long: -3.004031);

            // Not surpisingly, the same function can't be declared twice
            (double Latitude, double Longitude) GetSampleCoordinate(string address=null)
            {
                // In functions, the named properties are ignored.
                (double Latitude, double Longitude) result = 
                    address==null?(Latitude: 16.766123, Longitude: -3.004031) : // warning CS8123: The tuple element name 'Longitude' is ignored because a different name is specified by the target type '(double, double)'.
                    (Lat: 16.766123, Long: -3.004031); // warning CS8123: The tuple element name 'Long' is ignored because a different name is specified by the target type '(double, double)'.

                return result;
            }
            GetSampleCoordinate();
        }

        [TestMethod]
        public void CSharpCompileEnabled()
        {
            bool LocalFunction()
            {
                return true;
            }

            Assert.IsTrue(LocalFunction());
        }

    }
}
