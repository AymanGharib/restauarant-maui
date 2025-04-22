// Converters/BoolToStatusConverter.cs
namespace MauiMySQLDemo.Converters;

using System.Globalization;
using Microsoft.Maui.Controls;

public class BoolToStatusConverter : IValueConverter
{
    public object Convert(object value, Type t, object p, CultureInfo culture)
        => (bool)value ? "❌ Reserved" : "✅ Free";
    public object ConvertBack(object v, Type t, object p, CultureInfo c) => throw new NotImplementedException();
}
