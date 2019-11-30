using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Al_Quran.Models
{
    public class Juz_vm: INotifyPropertyChanged
    {
        private long _number;
        private List<Ayah_vm> _ayahs = new List<Ayah_vm>();
        private Dictionary<string, Surah_vm> _surahs = new Dictionary<string, Surah_vm>();
        private Edition_vm _edition { get; set; }
        private string _arabicTitle;
        private string _englishTitle;







        public string ArabicTitle
        {
            get { return _arabicTitle; }
            set { _arabicTitle = value; RaisePropertyChanged(); }
        }

        public string EnglishTitle
        {
            get { return _englishTitle; }
            set { _englishTitle = value; RaisePropertyChanged(); }
        }

        public long Number
        {
            get { return _number; }
            set { _number = value; RaisePropertyChanged(); }
        }

        public List<Ayah_vm> Ayahs
        {
            get { return _ayahs; }
            set { _ayahs = value; RaisePropertyChanged(); }
        }
     
        public Dictionary<string, Surah_vm> Surahs
        {
            get { return _surahs; }
            set { _surahs = value; RaisePropertyChanged(); }
        }

        public Edition_vm Edition
        {
            get { return _edition; }
            set { _edition = value; RaisePropertyChanged(); }
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
