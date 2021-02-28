using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;

namespace Wpf_Ui_TreeViews
{
    [ValueConversion(typeof(string),typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new  HeaderToImageConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;

            if (path == null)
                return null;

            var name = Path.GetDirectoryName(path);
            var image = "file.png";
            if (string.IsNullOrEmpty(name))
            {
                image = "drive.png";
            }
            else if( new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
                image = "folder-closed.png";
           
            return new BitmapImage( new Uri($"pack://application:,,,/Images/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
