using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CSharp7
{
    public partial class PathInfo
    {
        public PathInfo(
            string directoryName, string fileName, string extension)
        {
            (DirectoryName, FileName, Extension) =
                (directoryName, fileName, extension);
        }

        public PathInfo(string path)
        {
            (DirectoryName, FileName, Extension) = SplitPath(path);
        }



        static public (string DirectoryName, string FileName, string Extension) SplitPath(string path)
        {
            // Set https://github.com/dotnet/corefx/blob/master/src/System.Runtime.Extensions/src/System/IO for real implementation.

            return (
                System.IO.Path.GetDirectoryName(path),
                System.IO.Path.GetFileNameWithoutExtension(path),
                System.IO.Path.GetExtension(path)
                );
        }

        // ERROR: You can't override by return values - even on Tuples :)
        /*
        public (string PathRoot, string DirectoryName, string FileNameWithoutExtension)
            SplitPath(string path)
        {
            return (
                Path.GetPathRoot(path),
                Path.GetDirectoryName(path),
                Path.GetFileNameWithoutExtension(path),
                Path.GetExtension(path)
                );
        }
        */
    }

    [TestClass]
    public class ValueTupleTests
    {
        [TestMethod]
        public void Constructor_CreateTuple()
        {
            (string DirectoryName, string FileName, string Extension) pathData =
                (DirectoryName: @"\\test\unc\path\to", 
                FileName: "something", 
                Extension: ".ext");

            Assert.AreEqual<string>(
                @"\\test\unc\path\to", pathData.DirectoryName);
            Assert.AreEqual<string>(
                "something", pathData.FileName);
            Assert.AreEqual<string>(
                ".ext", pathData.Extension);

            Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                (DirectoryName: @"\\test\unc\path\to", 
                    FileName: "something", Extension: ".ext"),
                (pathData));

            Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                (@"\\test\unc\path\to", "something", ".ext"),
                (pathData));

            Assert.AreEqual<(string, string, string)>(
                (@"\\test\unc\path\to", "something", ".ext"),
                (pathData));

            Assert.AreEqual<Type>(typeof(ValueTuple<string, string, string>), 
                pathData.GetType());
        }

        [TestMethod]
        public void Constructor_OmitItemName()
        {
            (string DirectoryName, string FileName, string Extension) pathData =
                (DirectoryName: @"\\test\unc\path\to",
                FileName: "something",
                Extension: ".ext");

            Assert.AreEqual<string>(
                @"\\test\unc\path\to", pathData.Item1);
            Assert.AreEqual<string>(
                "something", pathData.Item2);
            Assert.AreEqual<string>(
                ".ext", pathData.Item3);

            Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                (DirectoryName: @"\\test\unc\path\to",
                    FileName: "something", Extension: ".ext"),
                (pathData));

            Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                (@"\\test\unc\path\to", "something", ".ext"),
                (pathData));

            Assert.AreEqual<(string, string, string)>(
                (@"\\test\unc\path\to", "something", ".ext"),
                (pathData));

            Assert.AreEqual<Type>(typeof(ValueTuple<string, string, string>),
                pathData.GetType());
        }
        [TestMethod]
        public void DeclaringTuples_WithMistmatchedTuplePropertyNames_IssuesWarning()
        {
            CompilerAssert.StatementsFailCompilation(
                @"
                (string DirectoryName, string FileName, string Extension) pathData =
                    (AltDirectoryName1: @""\\test\unc\path\to"", FileName: ""something"", Extension: "".ext"");
                pathData = (AltDirectoryName2: @""\\test\unc\path\to"", 
                    FileName: ""something"", Extension:"".ext"");
                pathData.GetType();
                ",
                @"Warning CS8123: The tuple element name 'AltDirectoryName1' is ignored because a different name is specified by the target type '(string DirectoryName, string FileName, string Extension)'.",
                "Warning CS8123: The tuple element name 'AltDirectoryName2' is ignored because a different name is specified by the target type '(string DirectoryName, string FileName, string Extension)'."
            );
            CompilerAssert.StatementsFailCompilation(
                @"
                (string, string, string) pathData =
                    (DirectoryName: @""\\test\unc\path\to"",
                    FileName: ""something"",
                    Extension: "".ext"");
                pathData.GetType();
                ",
            "Warning CS8123: The tuple element name 'DirectoryName' is ignored because a different name is specified by the target type '(string, string, string)'.",
            "Warning CS8123: The tuple element name 'Extension' is ignored because a different name is specified by the target type '(string, string, string)'.",
            "Warning CS8123: The tuple element name 'FileName' is ignored because a different name is specified by the target type '(string, string, string)'.");
        }

        [TestMethod]
        public void ValueTuple_GivenNamedTuple_ItemXHasSameValuesAsNames()
        {
            var normalizedPath = 
                (DirectoryName: @"\\test\unc\path\to", FileName: "something", 
                Extension: ".ext");

            Assert.AreEqual<string>(normalizedPath.Item1, normalizedPath.DirectoryName);
            Assert.AreEqual<string>(normalizedPath.Item2, normalizedPath.FileName);
            Assert.AreEqual<string>(normalizedPath.Item3, normalizedPath.Extension);
        }

        [TestMethod]
        public void ValueTuple_GivenNamedTuple_EquivalentToUnamedTupleWithSameValues()
        {
            var left = (DirectoryName: @"\\test\unc\path\to", FileName: "something", Extension: ".ext");
            var right = (@"\\test\unc\path\to", "something", ".ext");

            Assert.AreEqual<(string, string, string)>(
                left, right);
        }

    }
}
