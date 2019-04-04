using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FinServiceApp
{
    public class HighlightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int outParameter;
            int.TryParse(parameter.ToString(), out outParameter);

            int outValue;
            int.TryParse(value.ToString(), out outValue);

            return (outParameter == outValue);

            //return ((int)parameter == (int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? parameter : null;
        }
    }
}
