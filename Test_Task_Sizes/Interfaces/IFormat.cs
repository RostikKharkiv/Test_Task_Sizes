using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task_Sizes
{
    internal interface IFormat
    {
        string OutputFormat(long size);
    }
}