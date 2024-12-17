using System;
using System.Globalization;
using System.Windows.Data;

namespace PokeWish.MVVM.Converters
{
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Retourne false si la valeur est null, true sinon
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ConvertBack n'est pas utilisé dans ce cas.");
        }
    }
}
