using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp7
{
    [TestClass]
    public class InlineOutVariableDeclaration

    {
        [TestMethod]
        public void Deconstruct_GivenFileNamePathInfo_SeparateDirectoryNameFileNameExtension()
        {
            // E.g. 1: Out inline declaration and assignment
            PathInfo.SplitPath(@"\\test\unc\path\to\something.ext",
                out string directoryName,
                out string fileName,
                out string extension);

            Assert.AreEqual<string>(@"\\test\unc\path\to", directoryName);
            Assert.AreEqual<string>("something", fileName);
            Assert.AreEqual<string>(".ext", extension);
        }
    }
}
