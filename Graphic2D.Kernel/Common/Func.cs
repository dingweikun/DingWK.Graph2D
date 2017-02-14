using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Graphic2D.Kernel.Common
{
    public static class Func
    {
        public static bool IsZero(double value, int precision = 10)
        {
            double delt = 1.0;
            for (int i = 0; i < precision; i++)
                delt /= 10;
            return -delt < value && value < delt;
        }

        public static string FormatZeroString(double value, int precision)
        {
            return IsZero(value, precision) ? "0" : value.ToString();
        }

        public static Vector VectorRotate(double angle, double dx, double dy)
        {
            double cos = Math.Cos(angle / 180 * Math.PI);
            double sin = Math.Sin(angle / 180 * Math.PI);
            return new Vector(cos * dx + sin * dy, cos * dy - sin * dx);
        }
    }
}
