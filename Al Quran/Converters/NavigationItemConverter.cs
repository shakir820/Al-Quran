using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Al_Quran.Converters
{
    public class CurrNavigationItemFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (bool)value;
            if (val == true)
            {
                return Windows.UI.Text.FontWeights.SemiBold;
            }
            else
            {
                return Windows.UI.Text.FontWeights.Normal;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }




    public class CurrNavigationItemFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (bool)value;
            if (val == true)
            {
                return 20.0;
            }
            else
            {
                return 16.0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
