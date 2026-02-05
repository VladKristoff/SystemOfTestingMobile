using System.Globalization;

namespace SystemOfTestingMobile.Converters
{
    public class AnswerBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected && isSelected)
            {
                return Color.FromArgb("#E3F2FD"); // Светло-голубой для выбранного
            }
            return Colors.White; // Белый для невыбранного
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AnswerBorderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected && isSelected)
            {
                return Color.FromArgb("#007AFF"); // Синий для выбранного
            }
            return Color.FromArgb("#BDBDBD"); // Серый для невыбранного
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AnswerTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected && isSelected)
            {
                return Color.FromArgb("#007AFF"); // Синий для выбранного
            }
            return Color.FromArgb("#333333"); // Темный для невыбранного
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}