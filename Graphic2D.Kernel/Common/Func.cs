using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphic2D.Kernel.Common
{
    public static class Func
    {
        public static bool IsZero(double value)
        {
            return -1E-10 < value && value < 1E-10;
        }

        public static string FormatZeroString(double value)
        {
            return IsZero(value) ? "0" : value.ToString();
        }
    }
}
