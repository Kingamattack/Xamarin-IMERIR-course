// File: ListViewHeightConverter.cs
// Author: Jordy Kingama
// Date: 3/3/2020

using System;
using System.Globalization;

using Xamarin.Forms;

namespace tvshows.Converters
{
    public class ListViewHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int count = (int)value;

            return count * 75;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}