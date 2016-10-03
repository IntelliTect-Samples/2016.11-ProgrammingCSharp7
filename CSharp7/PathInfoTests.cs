using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp7
{
    [TestClass]
    public class PathInfoTests
    {
        [TestMethod]
        public void SplitPath_GivenPath_SuccessfullySplitsIntoConsituentNamedParts()
        {
            (string directoryName, string fileName, string extension) =
                PathInfo.SplitPath(@"\\test\unc\path\to\something.ext");

            Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                (@"\\test\unc\path\to", "something",".ext"),
                (directoryName, fileName, extension));
        }

        [TestMethod]
        public void SplitPath_GivenPath_SuccessfullySplitsIntoSingleTuple()
        {
            var normalizedPath =
                PathInfo.SplitPath(@"\\test\unc\path\to\something.ext");

            Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                (@"\\test\unc\path\to", "something", ".ext"),
                (normalizedPath.DirectoryName, normalizedPath.FileName, normalizedPath.Extension));
        }

        [TestMethod]
        public void Deconstruct_GivenFileNamePathInfo_SeparateDirectoryNameFileNameExtension()
        {
            PathInfo pathInfo = new PathInfo(@"\\test\unc\path\to\something.ext");

            {
                // E.g. 1: Deconstructing declaration and assignment
                (string directoryName, string fileName, string extension) = pathInfo;
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

            /* ERROR: Assignment to ValueType is not supported implicitly.
            {
                // E.g. 4: Deconstructing declaration and assignment to a ValueTuple
                (string DirectoryName, string FileName, string Extension) path = pathInfo;
                VerifyExpectedValue(directoryName, fileName, extension);
            }
            */

            void VerifyExpectedValue(string directoryName, string fileName, string extension)
            {
                Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                    (@"\\test\unc\path\to", "something", ".ext"),
                    (directoryName, fileName, extension));
            }

        }

        /* 
        [TestMethod]
        public void Deconstruct_GivenPathInfo_DeconstructingDeclarationAndAssignment()
        {
            PathInfo pathInfo = new PathInfo(@"\\test\unc\path\to\something.ext");

            // deconstructing declaration and assignment
            (FileInfo path) = pathInfo;

            Assert.AreEqual<(string DirectoryName, string FileName, string Extension)>(
                (@"\\test\unc\path\to", "something", ".ext"),
                (directoryName, fileName, extension));
        }
        */

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
