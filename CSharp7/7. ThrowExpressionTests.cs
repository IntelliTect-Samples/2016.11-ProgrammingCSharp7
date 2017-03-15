using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7
{
    public partial class TemporaryFile
    {
        public TemporaryFile(string fileName) =>
            File = new FileInfo(
                fileName ?? throw new ArgumentNullException());
    }
}
