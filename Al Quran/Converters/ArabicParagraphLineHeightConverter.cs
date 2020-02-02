using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Al_Quran.Converters
{
    public class ArabicParagraphLineHeightConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var fontSize = (int)value;

            double height = 35;

            switch (fontSize)
            {
                case 16:
                    height = 35;
                    break;

                case 18:
                    height = 37;
                    break;

                case 20:
                    height = 39;
                    break;

                case 22:
                    height = 41;
                    break;

                case 24:
                    height = 43;
                    break;

                case 26:
                    height = 45;
                    break;

                case 28:
                    height = 47;
                    break;

                case 30:
                    height = 49;
                    break;

                default:
                    height = 35;
                    break;
            }

            return height;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
