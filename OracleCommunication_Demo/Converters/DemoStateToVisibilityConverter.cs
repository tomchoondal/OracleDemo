using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OracleCommunication_Demo.Converters
{
    public class DemoStateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            DemoType type;
            Enum.TryParse(value.ToString(), out type);
            var itemType = parameter == null ? string.Empty : parameter.ToString();
            switch (itemType)
            {
                case "BasicGuidance":
                    return (type == DemoType.BasicGuidance) ? Visibility.Visible : Visibility.Collapsed;
                case "RemoteSupport":
                    return (type == DemoType.RemoteSupport) ? Visibility.Visible : Visibility.Collapsed;
                case "PersonalShopper":
                    return (type == DemoType.Personal_Shopper) ? Visibility.Visible : Visibility.Collapsed;
                case "Concierge":
                    return (type == DemoType.Concierge) ? Visibility.Visible : Visibility.Collapsed;
                case "Collaboration":
                    return (type == DemoType.Collaboration) ? Visibility.Visible : Visibility.Collapsed;
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
