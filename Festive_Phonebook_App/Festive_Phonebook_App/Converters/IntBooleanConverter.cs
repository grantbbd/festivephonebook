using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Festive_Phonebook_App.Converters
{
    public class IntBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value == 1;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
