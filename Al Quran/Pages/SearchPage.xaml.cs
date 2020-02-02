using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Al_Quran.Models;
using System.Collections.ObjectModel;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Al_Quran.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page, INotifyPropertyChanged
    {
        public SearchPage()
        {
            this.InitializeComponent();

            QuranFunctionality = QuranFunctionality.Current;
        }




        private QuranFunctionality quranFunctionality;
        private ObservableCollection<Surah_vm> surahCollection = new ObservableCollection<Surah_vm>();



        public QuranFunctionality QuranFunctionality
        {
            get { return quranFunctionality; }
            set { quranFunctionality = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<Surah_vm> SurahCollection
        {
            get { return surahCollection; }
            set { surahCollection = value; RaisePropertyChanged(); }
        }






        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var listOfSurah = (List<Surah_vm>) e.Parameter;

            SurahCollection = new ObservableCollection<Surah_vm>(listOfSurah);
        }







        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName]string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void SuraListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var surah = (Surah_vm)SuraListView.SelectedItem;
            Frame.Navigate(typeof(SurahViewPage), surah);
        }
    }
}
