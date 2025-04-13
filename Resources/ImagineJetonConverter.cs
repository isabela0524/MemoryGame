using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MemoryGame2.Resources
{
    public class ImagineJetonConverter:IValueConverter
    {
        private static readonly string DosJetonPath = "Images/back.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imagine = value as string;
            var esteDescoperit = parameter as bool?;

            if(parameter is bool esteVizibil && esteVizibil && !string.IsNullOrEmpty(imagine))
            {
                return new BitmapImage(new Uri(imagine, UriKind.RelativeOrAbsolute));
            }

            return new BitmapImage(new Uri(DosJetonPath, UriKind.Relative));
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;

    }
}
