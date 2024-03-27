using System.Globalization;

namespace ShoppingListMobile.Converters;
public class TextDecorationStrikethroughConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((bool)value)
        {
            return TextDecorations.Strikethrough;
        }
        return TextDecorations.None;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
