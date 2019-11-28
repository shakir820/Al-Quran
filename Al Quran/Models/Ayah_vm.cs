using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Al_Quran.Models
{
    public class Ayah_vm: INotifyPropertyChanged
    {

        private long _number;
        private string _text;
        private Surah_vm _surah;
        private long _numberInSurah;
        private long _juz;
        private long _manzil;
        private long _page;
        private long _ruku;
        private long _hizbQuarter;
        private Sajda_vm _sajda;


        public long Number
        {
            get { return _number; }
            set { _number = value; RaisePropertyChanged(); }
        }
        public string Text
        {
            get { return _text; }
            set { _text = value; RaisePropertyChanged(); }
        }
        public Surah_vm Surah
        {
            get { return _surah; }
            set { _surah = value; RaisePropertyChanged(); } 
        }
        public long NumberInSurah
        {
            get { return _numberInSurah; }
            set { _numberInSurah = value; RaisePropertyChanged(); }
        }
        public long Juz
        {
            get { return _juz; }
            set{ _juz = value; RaisePropertyChanged(); }
        }
        public long Manzil
        {
            get { return _manzil; }
            set { _manzil = value; RaisePropertyChanged();} 
        }
        public long Page
        {
            get { return _page; }
            set { _page = value;   RaisePropertyChanged(); }
        }
        public long Ruku
        {
            get { return _ruku; } 
            set{ _ruku = value; RaisePropertyChanged(); }
        }
        public long HizbQuarter
        {
            get { return _hizbQuarter; }
            set { _hizbQuarter = value; RaisePropertyChanged(); }
        }
        public Sajda_vm Sajda
        {
            get { return _sajda; }
            set { _sajda = value; RaisePropertyChanged(); }
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
