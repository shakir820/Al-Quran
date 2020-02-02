using Al_Quran.Models.CommunicationWithServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;

namespace Al_Quran.Models
{
    public class QuranFunctionality: INotifyPropertyChanged
    {
        public static QuranFunctionality Current;
        private ObservableCollection<Surah_vm> _suraCollection = new ObservableCollection<Surah_vm>();
        private ObservableCollection<Juz_vm> _juzCollection = new ObservableCollection<Juz_vm>();
        private CoreDispatcher Dispatcher;
        private FontSizeVM fontSizeVM = new FontSizeVM();



        public FontSizeVM FontSizeVM
        {
            get { return fontSizeVM; }
            set { fontSizeVM = value; RaisePropertyChanged(); }
        }

        public string SelectedTranslationIdentifier { get; set; } = null;

        public ObservableCollection<Surah_vm> SuraCollection
        {
            get { return _suraCollection; }
            set { _suraCollection = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<Juz_vm> JuzCollection
        {
            get { return _juzCollection; }
            set { _juzCollection = value; RaisePropertyChanged(); }
        }

        public bool IsSuraListPopulating { get; set; } = false;

        public bool IsSuraListPopulated { get; set; } = false;



        #region events
        public event EventHandler<SuraListPopulatedEventArgs> SuraListPopulatedChanged;
        #endregion




        public QuranFunctionality()
        {
            Current = this;
            Dispatcher = CoreApplication.GetCurrentView().Dispatcher;
           
        //Get data
            Task.Run(() =>
            {
                FetchSuraListFromServer();
            });

            GetAppSettings();

            Task.Run(() =>{ PopulateJuzList(); });
        }






        public void GetAppSettings()
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey("FontSize"))
            {
                FontSizeVM.FontSize = (int)localSettings.Values["FontSize"];
            }
        }


        public void SaveAppSettings()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["FontSize"] = FontSizeVM.FontSize;
        }





        public async void FetchSuraListFromServer()
        {
            IsSuraListPopulating = true;
            var result = await App.AlQuranCloudServer.GetSuraListAsync();
            if (result.surahs != null)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { SuraCollection = result.surahs; });
            }
            else if (result.internetError == true)
            {
                SuraListPopulatedChanged?.Invoke(this, new SuraListPopulatedEventArgs() { InternetError = true, SuraListPopulated = true });
                IsSuraListPopulating = false;
                IsSuraListPopulated = true;
                return;
            }
            IsSuraListPopulating = false;
            IsSuraListPopulated = true;
            SuraListPopulatedChanged?.Invoke(this, new SuraListPopulatedEventArgs() { InternetError = false, SuraListPopulated = true });
        }




        private async void PopulateJuzList()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => 
            {
                for (var i = 0; i < 30; i++)
                {
                    var count = i + 1;
                    Juz_vm juz = new Juz_vm();
                    juz.Number = count;
                    juz.EnglishTitle = $"Juz {count}";
                    JuzCollection.Add(juz);

                }
            });
           
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


    public class SuraListPopulatedEventArgs: EventArgs
    {
        public bool InternetError = false;
        public bool SuraListPopulated = false;

    }
}
