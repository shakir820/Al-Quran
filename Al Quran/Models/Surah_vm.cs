using Al_Quran.Models.API_Data;
using Al_Quran.Models.CommunicationWithServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Ayah_vm> _ayahs = new ObservableCollection<Ayah_vm>();
        private Edition_vm _edition;







        public ObservableCollection<Ayah_vm> Ayahs
        {
            get { return _ayahs; }
            set { _ayahs = value; RaisePropertyChanged(); }
        }
      
        public Edition_vm Edition
        {
            get { return _edition; }
            set { _edition = value; RaisePropertyChanged(); }
        }

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




        public  void GetAyah()
        {
             Task.Run(async () => { var result = await App.AlQuranCloudServer.GetSurah((int)Number, this); });
        }


        public void GetAyahTextTranslation(string identifier = "en.asad")
        {
            Task.Run(async () => { var result = await App.AlQuranCloudServer.GetSurahTextTranslation((int)Number, this, identifier); });
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
