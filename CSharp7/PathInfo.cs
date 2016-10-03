using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7
{
    public class PathInfo
    {
        public string DirectoryName { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }

    public string Path
    {
        get { return System.IO.Path.Combine(DirectoryName, FileName, Extension); }
    }
        /*  ERROR: Expression bodied members not yet supported.
        public string Path
        {
            get => System.IO.Path.Combine(DirectoryName, FileName, Extension);
        }
        */


        public PathInfo(string directoryName, string fileName, string extension)
        {
            (DirectoryName, FileName, Extension) =
                (directoryName, fileName, extension);
        }

        public PathInfo(string path)
        {
            (DirectoryName, FileName, Extension) = SplitPath(path);
        }

        public void Deconstruct(out string directoryName, out string fileName, out string extension)
        {
            (directoryName, fileName, extension) = (DirectoryName, FileName, Extension);
        }

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

        static public (string DirectoryName, string FileName, string Extension)
    SplitPath(string path)
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
}
