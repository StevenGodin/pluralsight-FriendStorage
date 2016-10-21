using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using FriendStorage.UI.DataProvider.Lookups;

namespace FriendStorage.UI.Converters
{
    public class ComboBoxOriginalValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = (int) value;
            var comboBox = parameter as ComboBox;

            var lookupItem = comboBox?.ItemsSource?.OfType<LookupItem>().SingleOrDefault(l => l.Id == id);
            return lookupItem != null
                ? lookupItem.DisplayValue
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}