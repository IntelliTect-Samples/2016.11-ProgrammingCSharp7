using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp7;

namespace SampleClassLibary
{
    public class Sample
    {
        public void Method()
        {
            CSharp7Tests thing = new CSharp7Tests();
            Trace.Assert(thing.GetCurrentCoordinate().Elevation == 279);
        }
    }
}
