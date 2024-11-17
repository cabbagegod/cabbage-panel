using System.Windows.Data;

namespace CHPanel.Helpers;

// Thanks https://stackoverflow.com/a/1039681 :)

[ValueConversion(typeof(bool), typeof(bool))]
public class InverseBoolConverter : IValueConverter {
	public object Convert(object value, Type targetType, object parameter,
		System.Globalization.CultureInfo culture) {
		if (targetType != typeof(bool))
			throw new InvalidOperationException("The target must be a boolean");

		return !(bool)value;
	}

	public object ConvertBack(object value, Type targetType, object parameter,
		System.Globalization.CultureInfo culture) {
		throw new NotSupportedException();
	}
}