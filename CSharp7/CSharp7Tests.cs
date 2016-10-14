using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp7
{
    [TestClass]
    public class CSharp7Tests
    {
        // Confirm C# 7.0 is active using a binary literal
        long LargestSquareNumberUsingAllDigits = 
            0b0010_0100_1000_1111_0110_1101_1100_0010_0100;  // 9,814,072,356
        // Confirm C# 7.0 is active digit separator
        long MaxInt64 { get; } = 9_223_372_036_854_775_807;  // Equivalent to long.MaxValue

        (int Height, int Width, int Length) DefaultCubeSize = (1, 2, 3);  // Tuples

        #region Out Parameter Declaration
        public long DivideWithRemainder(
            long numerator, long denominator, out long remainder)
        {
            remainder = numerator % denominator;
            return (numerator / denominator);
        }

        [TestMethod]
        public void DivideTest()
        {
            Assert.AreEqual<long>(21, 
                DivideWithRemainder(42, 2, out long remainder));
            Assert.AreEqual<long>(0, remainder);

             // This is another great place for tuples but 
            // I didn't use them to avoid overlapping C# 7.0 features.
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
        public void OutScopePreventsReusingLocalVariableName()
        {
            // ERROR: A local variable named 'remainder' is already defined in the scope.
            CompilerAssert.StatementsFailCompilation(
            @"int.TryParse(""42"", out int result);
            int.TryParse(""42"", out int result);",
            "Error CS0128: A local variable named 'result' is already defined in this scope");

        }
        #endregion // Out Parameter Declaration


        #region Tuples
        [TestMethod][Ignore]
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
        #endregion // Tuples

        System.Threading.Tasks.ValueTask<T> Blah<T>()
        {
            return null;
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
