using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task_Sizes
{
    abstract class OutputTo : IOutput
    {
        public abstract void Output(string outputFilePath, string info);
    }

    internal class DefaultOutput : OutputTo
    {
        public override void Output(string outputFilePath, string info)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                writer.Write(info);
            }

            Console.WriteLine(info);
        }
    }

    internal class WhithoutConsoleOutput : OutputTo
    {
        public override void Output(string outputFilePath, string info)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                writer.Write(info);
            }
        }
    }
}
