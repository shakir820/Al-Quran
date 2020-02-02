using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Al_Quran.Models
{
    public class FontSizeVM: INotifyPropertyChanged
    {
        public static FontSizeVM Current;


        public FontSizeVM()
        {
            Current = this;
        }

        private int _fontSize = 16;

        public int FontSize 
        { 
            get { return _fontSize; } 
            set 
            { 
                if(_fontSize != value)
                {
                    _fontSize = value;
                    RaisePropertyChanged();
                    RiseFontSizeChangedEvent();
                    //CalculateParagraphHeight(value);
                }
            } 
        }

        private void CalculateParagraphHeight(int value)
        {
            
        }

        private void RiseFontSizeChangedEvent()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["FontSize"] = _fontSize;

            FontSizeChanged?.Invoke(this, new EventArgs());
        }



        public event EventHandler FontSizeChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName]string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
