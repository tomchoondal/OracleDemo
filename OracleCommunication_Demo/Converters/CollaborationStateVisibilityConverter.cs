using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OracleCommunication_Demo.Converters
{
    class CollaborationStateVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            CollaborationStates state;
            Enum.TryParse(value.ToString(), out state);
            var itemType = parameter == null ? string.Empty : parameter.ToString();
            switch (itemType)
            {
                case "VideoExpanded":
                    return (state == CollaborationStates.VideoExpanded) ? Visibility.Visible : Visibility.Collapsed;
                case "AgentExpanded":
                    return (state == CollaborationStates.AgentExpanded) ? Visibility.Visible : Visibility.Collapsed;
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
