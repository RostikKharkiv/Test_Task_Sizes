using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task_Sizes
{
    abstract class Format : IFormat
    {
        public abstract string OutputFormat(long size);
    }

    internal class DefaultFormat : Format
    {
        public override string OutputFormat(long size)
        {
            return $"{size} bytes";
        }
    }

    internal class HumanFormat : Format
    {
        public override string OutputFormat(long size)
        {
            switch (size)
            {
                case <= 1024 * 1024:
                    return string.Format("{0:f}", (double) size / 1024) + "KB";
                case <= 1024 * 1024 * 1024:
                    return string.Format("{0:f}", (double)size / (1024 * 1024)) + "MB";
                case <= (long)1024 * 1024 * 1024 * 1024:
                    return string.Format("{0:f}", (double)size / (1024 * 1024 * 1024)) + "GB";

                default:
                    return $"{size} bytes";
            }
        }
    }
}
