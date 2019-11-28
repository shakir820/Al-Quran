using Al_Quran.Models.CommunicationWithServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Al_Quran.Models
{
    public class QuranFunctionality: INotifyPropertyChanged
    {
        public static QuranFunctionality Current;

        private ObservableCollection<Surah_vm> _suraCollection = new ObservableCollection<Surah_vm>();

        private CoreDispatcher Dispatcher;




        public ObservableCollection<Surah_vm> SuraCollection
        {
            get { return _suraCollection; }
            set { _suraCollection = value; RaisePropertyChanged(); }
        }


        public QuranFunctionality()
        {
            Current = this;
            Dispatcher = CoreApplication.GetCurrentView().Dispatcher;
            
            //Get data
            Task.Run(async () =>
            {
                var result = await AlQuranCloudServer.GetSuraListAsync();
                if (result != null)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { SuraCollection = result; });
                }
            });

            Task.Run(async () =>
            {
                var result = await AlQuranCloudServer.GetSuraListAsync();
                if (result != null)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { SuraCollection = result; });
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
}
