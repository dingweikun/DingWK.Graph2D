using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DingWK.Graphic2D.Converters
{
    public class ScrollBarValuesToPageOffset : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values?.Length == 2)
                return new Point(-(double)values[0], -(double)values[1]);
            else
                return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            Point offset = (Point)value;
            return new object[2] { -offset.X, -offset.Y };
        }
    }

    public class PageOffsetToHorScrollBarValue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -((Point)value).X;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Point back = (Point)parameter;
            back.X = -(double)value;
            return back;
        }
    }


    public class DoubleNegativeConverter : IValueConverter
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