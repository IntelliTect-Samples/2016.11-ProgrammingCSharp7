using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp7
{
    [TestClass]
    public class ValueTupleTests
    {
        [TestMethod]
        public void ValueTuple_GivenNamedTuple_ItemXHasSameValuesAsNames()
        {
            var normalizedPath = (DirectoryName: @"\\test\unc\path\to", FileName: "something", Extension: ".ext");

            Assert.AreEqual<string>(normalizedPath.Item1, normalizedPath.DirectoryName);
            Assert.AreEqual<string>(normalizedPath.Item2, normalizedPath.FileName);
            Assert.AreEqual<string>(normalizedPath.Item3, normalizedPath.Extension);
        }

        [TestMethod]
        public void ValueTuple_GivenNamedTuple_EquipvalentToUnamedTupleWithSameValues()
        {
            var left = (DirectoryName: @"\\test\unc\path\to", FileName: "something", Extension: ".ext");
            var right = (@"\\test\unc\path\to", "something", ".ext");

            Assert.AreEqual<(string, string, string)>(
                left, right);
        }
    }
}
