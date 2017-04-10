using System;
using System.Globalization;
using System.Windows.Data;

namespace DingWK.Graphic2D.Converters
{
    internal class DoubleNegativeConverter : IValueConverter
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