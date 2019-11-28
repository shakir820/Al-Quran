using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Al_Quran.Models
{
    public class Surah_vm: INotifyPropertyChanged
    {
        private long _number;
        private string _name;
        private string _englishName;
        private string _englishNameTranslation;
        private long _numberOfAyahs;
        private string _revelationType;





        public long Number
        {
            get { return _number; }
            set { _number = value; RaisePropertyChanged(); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }
        public string EnglishName
        {
            get { return _englishName; }
            set { _englishName = value; RaisePropertyChanged(); }
        }
        public string EnglishNameTranslation
        {
            get { return _englishNameTranslation; }
            set { _englishNameTranslation = value; RaisePropertyChanged(); }
        }
        public long NumberOfAyahs
        {
            get { return _numberOfAyahs; }
            set { _numberOfAyahs = value; RaisePropertyChanged(); }
        }
        public string RevelationType
        {
            get { return _revelationType; }
            set { _revelationType = value; RaisePropertyChanged(); }
        }


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
