// File: CollectionViewHeightConverter.cs
// Author: Jordy Kingama
// Date: 3/3/2020

using System;
using System.Globalization;

using Xamarin.Forms;

namespace tvshows.Converters
{
    public class CollectionViewHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int count = (int)value;
            int rows = count / 3 + 1;

            return rows * 165;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
