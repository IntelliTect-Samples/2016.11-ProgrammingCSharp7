using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7
{
    class TemporaryFile
    {
        public TemporaryFile(string fileName) =>
            File = new FileInfo(
                fileName ?? throw new ArgumentNullException());
        ~TemporaryFile() => Dispose();
        FileInfo _File;
        public FileInfo File
        {
            get => _File;
            set => _File = value;
        }
        void Dispose() => File?.Delete();
    }
}
