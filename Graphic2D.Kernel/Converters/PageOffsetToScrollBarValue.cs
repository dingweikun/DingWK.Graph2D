using System;
using System.Globalization;
using System.Windows.Data;

namespace Graphic2D.Kernel.Converters
{
    public class PageOffsetToScrollBarValue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -(double)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -(double)value;
        }
    }
}
