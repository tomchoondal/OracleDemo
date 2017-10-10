using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace OracleCommunication_Demo.Converters
{
    public class VideoFallbacktoVisibilityConverter  :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            VideoFallBackType type;
            Enum.TryParse(value.ToString(), out type);
            var itemType = parameter == null ? string.Empty : parameter.ToString();
            switch (itemType)
            {
                case "None":
                    return Visibility.Collapsed;
                case "Reconnecting":
                    return (type == VideoFallBackType.Reconnecting) ? Visibility.Visible : Visibility.Collapsed;
                case "Reconnectionfailed":
                    return (type == VideoFallBackType.Reconnectionfailed) ? Visibility.Visible : Visibility.Collapsed;
                default:
                    return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
