using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7
{
    public partial class PathInfo
    {
        
        //  ERROR: Expression bodied members not yet supported.
        public string Path
        {
            get => System.IO.Path.Combine(DirectoryName, FileName, Extension);
        }
        

    }
}
