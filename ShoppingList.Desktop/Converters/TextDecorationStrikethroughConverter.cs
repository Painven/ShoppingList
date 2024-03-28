using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ShoppingList.Desktop.Converters;
public class TextDecorationStrikethroughConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((bool)value)
        {
            return TextDecorations.Strikethrough;
        }
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
