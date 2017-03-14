using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp7
{
    public partial class PathInfo
    {

        public string Path
        {
            get { return System.IO.Path.Combine(DirectoryName, FileName, Extension); }
        }

        public void Deconstruct(
            out string directoryName, 
            out string fileName, 
            out string extension)
        {
            directoryName = DirectoryName;
            fileName = FileName;
            extension = Extension;
        }

        #region Single parameter constructors are not supported as deconstructors
        public void Deconstruct(out string path)
        {
            path = Path;
        }
        public void Deconstruct(out FileInfo file)
        {
            file = new FileInfo(Path);
        }
        public void Deconstruct(out DirectoryInfo directory)
        {
            directory = new DirectoryInfo(Path);
        }
        #endregion Single parameter constructors are not supported as deconstructors
    }

    [TestClass]
    public partial class PathInfoTests
    {
        [TestMethod]
        public void SplitPath_GivenPath_SuccessfullySplitsIntoConsituentNamedParts()
        {


            PathInfo pathInfo = new PathInfo(@"\\test\unc\path\to\something.ext");

            
            (string directoryName, string fileName, string extension) = pathInfo;

            Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                (@"\\test\unc\path\to", "something",".ext"),  // Expected
                (directoryName, fileName, extension)          // Actual
                );
        }

        [TestMethod]
        public void MyTestMethod()
        {
            int? number = 5;
            if (number is int x)
            {
                Assert.AreEqual<int>(5, x);
            }
            else
                throw new Exception();
        }
        [TestMethod]
        public void MyTestMethod3()
        {
            int? number = null;
            if (number is int x)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void SplitPath_GivenPath_SuccessfullySplitsIntoSingleTuple()
        {
            var normalizedPath =
                PathInfo.SplitPath(@"\\test\unc\path\to\something.ext");

            Assert.AreEqual<(string, string , string )>(
                (@"\\test\unc\path\to", "something", ".ext"),
                (normalizedPath.DirectoryName, normalizedPath.FileName, normalizedPath.Extension));
        }

        [TestMethod]
        public void Deconstruct_GivenFileNamePathInfo_SeparateDirectoryNameFileNameExtension()
        {
            PathInfo pathInfo = new PathInfo(@"\\test\unc\path\to\something.ext");

            {

                // E.g. 1: Deconstructing declaration and assignment
                pathInfo.Deconstruct(
                    out string directoryName,
                    out string fileName,
                    out string extension);

                (directoryName, fileName, extension) = pathInfo;
                VerifyExpectedValue(directoryName, fileName, extension);
            }
            
            {
                string directoryName, fileName, extension = null;
                // E.g. 2: Deconstructing assignment
                (directoryName, fileName, extension) = pathInfo;
                VerifyExpectedValue(directoryName, fileName, extension);
            }

            {
                // E.g. 3: Deconstructing declaration and assignment with var
                var (directoryName, fileName, extension) = pathInfo;
                VerifyExpectedValue(directoryName, fileName, extension);
            }

            void VerifyExpectedValue(string directoryName, string fileName, string extension)
            {
                Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                    (@"\\test\unc\path\to", "something", ".ext"),
                    (directoryName, fileName, extension));
            }

        }

        [TestMethod]
        public void Deconstruct_AssignmentToValueTypeIsNotSupported()
        {
            string[] expectedErrors = {
                        "Error CS0029: Cannot implicitly convert type 'CSharp7.PathInfo' to '(string DirectoryName, string FileName, string Extension)'"
                    };
            CompilerAssert.StatementsFailCompilation(
                @"
                    PathInfo pathInfo = new PathInfo(
                            @""\\test\unc\path\to\something.ext"");
                    // E.g. 4: Deconstructing declaration and assignment to a ValueTuple
                    (string DirectoryName, string FileName, string Extension) path = pathInfo;
                ",
                expectedErrors);

        }

        [TestMethod]
        public void Deconstruct_GivenSingleOutOParameter_FailsCompilation()
        {
            string[] expectedErrors = {
                        "Error CS1002: ; expected",
                        "Error CS1002: ; expected",
                        "Error CS1513: } expected",
                        "Error CS1525: Invalid expression term '='",
                        "Error CS0119: 'FileInfo' is a type, which is not valid in the given context",
                        "Error CS0119: 'FileInfo' is a type, which is not valid in the given context",
                        "Error CS0103: The name 'path' does not exist in the current context",
                        "Error CS1026: ) expected"
                    };


            CompilerAssert.StatementsFailCompilation(
                @"
                        PathInfo pathInfo = new PathInfo(
                            @""\\test\unc\path\to\something.ext"");
                        (FileInfo path) = pathInfo; 
                ",
                expectedErrors);
        }

        [TestMethod]
        public void Deconstruct_GivenPathInfo_DeconstructingAndAssignment()
        {
            string directoryName, fileName, extension = null;

            PathInfo pathInfo = new PathInfo(@"\\test\unc\path\to\something.ext");

            // deconstructing assignment
            (directoryName, fileName, extension) = pathInfo;

            Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                (@"\\test\unc\path\to", "something", ".ext"),
                (directoryName, fileName, extension));
        }

        [TestMethod]
        public void Deconstruct_GivenPathInfo_DeconstructingAndVarAssignment()
        {
            PathInfo pathInfo = new PathInfo(@"\\test\unc\path\to\something.ext");

            // deconstructing declaration and assignment with var
            var (directoryName, fileName, extension) = pathInfo;

            Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                (@"\\test\unc\path\to", "something", ".ext"),
                (directoryName, fileName, extension));
        }
    }
}
