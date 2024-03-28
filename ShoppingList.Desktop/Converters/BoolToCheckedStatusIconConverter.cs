using MahApps.Metro.IconPacks;
using System.Globalization;
using System.Windows.Data;

namespace ShoppingList.Desktop.Converters;
public class BoolToCheckedStatusIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value)
        {
            return PackIconMaterialKind.Check;
        }
        return PackIconMaterialKind.Clock;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
