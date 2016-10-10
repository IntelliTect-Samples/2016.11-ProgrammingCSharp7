using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp7
{
    [TestClass]
    public partial class ExpressionBodiedMemebersTests
    {
        [TestMethod]
        public void ExpressionBodiedMemberTests()
        {
            // IMPORTANT: This should work but the compiler doesn't appear to have implemented it yet.
            CompilerAssert.CodeFailCompilation(
                @"
                class TemporaryFile
                {
                    public TemporaryFile(string fileName) =>
                        File = new FileInfo(fileName??throw new ArgumentNullException());
                    ~TemporaryFile() => Dispose();
                    FileInfo _File; 
                    public FileInfo File 
                    { 
                        get => _File; 
                        set => _File = value;
                    }
                    void Dispose() => File?.Delete();
                }
                ",
                ExpressionBodiedMemberErrors
            );
        }

        #region ExpressionBodiedMemberErrors
        string ExpressionBodiedMemberErrors =
                @"Error CS0111: Type 'TemporaryFile' already defines a member called 'Dispose' with the same parameter types
Error CS0161: 'TemporaryFile.File.get': not all code paths return a value
Error CS0201: Only assignment, call, increment, decrement, and new object expressions can be used as a statement
Error CS0201: Only assignment, call, increment, decrement, and new object expressions can be used as a statement
Error CS0246: The type or namespace name 'fileName' could not be found (are you missing a using directive or an assembly reference?)
Error CS0501: 'TemporaryFile.~TemporaryFile()' must declare a body because it is not marked abstract, extern, or partial
Error CS0501: 'TemporaryFile.ArgumentNullException()' must declare a body because it is not marked abstract, extern, or partial
Error CS0501: 'TemporaryFile.Dispose()' must declare a body because it is not marked abstract, extern, or partial
Error CS0501: 'TemporaryFile.FileInfo(fileName)' must declare a body because it is not marked abstract, extern, or partial
Error CS0501: 'TemporaryFile.TemporaryFile(string)' must declare a body because it is not marked abstract, extern, or partial
Error CS1001: Identifier expected
Error CS1002: ; expected
Error CS1002: ; expected
Error CS1002: ; expected
Error CS1002: ; expected
Error CS1003: Syntax error, ',' expected
Error CS1026: ) expected
Error CS1043: { or ; expected
Error CS1513: } expected
Error CS1513: } expected
Error CS1519: Invalid token ')' in class, struct, or interface member declaration
Error CS1519: Invalid token '=' in class, struct, or interface member declaration
Error CS1519: Invalid token '=' in class, struct, or interface member declaration
Error CS1519: Invalid token '=>' in class, struct, or interface member declaration
Error CS1519: Invalid token '=>' in class, struct, or interface member declaration
Error CS1520: Method must have a return type
Error CS1520: Method must have a return type
Error CS1520: Method must have a return type
Warning CS0109: The member 'TemporaryFile.ArgumentNullException()' does not hide an inherited member. The new keyword is not required.
Warning CS0109: The member 'TemporaryFile.FileInfo(fileName)' does not hide an inherited member. The new keyword is not required.";
        #endregion ExpressionBodiedMemberErrors
    }
}