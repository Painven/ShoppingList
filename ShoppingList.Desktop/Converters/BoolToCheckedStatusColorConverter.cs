using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ShoppingList.Desktop.Converters;

public class BoolToCheckedStatusColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value)
        {
            return Brushes.LawnGreen;
        }
        return Brushes.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
