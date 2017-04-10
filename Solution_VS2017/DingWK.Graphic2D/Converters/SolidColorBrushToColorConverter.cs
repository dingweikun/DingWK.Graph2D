using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DingWK.Graphic2D.Converters
{
    internal class SolidColorBrushToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as SolidColorBrush)?.Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush((Color)value);
        }
    }
}
