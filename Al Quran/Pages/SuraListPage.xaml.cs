using Al_Quran.Models;
using Al_Quran.Models.CommunicationWithServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Al_Quran.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SuraListPage : Page, INotifyPropertyChanged
    {
        private QuranFunctionality quranFunctionality;


        public QuranFunctionality QuranFunctionality
        {
            get { return quranFunctionality; }
            set { quranFunctionality = value; RaisePropertyChanged(); }
        }



        public SuraListPage()
        {
            this.InitializeComponent();
            QuranFunctionality = QuranFunctionality.Current;
           
        }

        private async void QuranFunctionality_SuraListPopulated(object sender, SuraListPopulatedEventArgs e)
        {

            await Dispatcher.RunIdleAsync((a) =>
            {
                if(e.InternetError == true)
                {
                    SuraListView.Visibility = Visibility.Collapsed;
                    NoInternetGrid.Visibility = Visibility.Visible;
                    LoadingProgressBar.IsIndeterminate = false;
                    LoadingGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    SuraListView.Visibility = Visibility.Visible;
                    NoInternetGrid.Visibility = Visibility.Collapsed;
                    LoadingProgressBar.IsIndeterminate = false;
                    LoadingGrid.Visibility = Visibility.Collapsed;
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            QuranFunctionality.SuraListPopulatedChanged += QuranFunctionality_SuraListPopulated;

            if (QuranFunctionality.IsSuraListPopulated)
            {
                SuraListView.Visibility = Visibility.Visible;
                NoInternetGrid.Visibility = Visibility.Collapsed;
                LoadingProgressBar.IsIndeterminate = false;
                LoadingGrid.Visibility = Visibility.Collapsed;
            }
            else if (QuranFunctionality.IsSuraListPopulating)
            {
                SuraListView.Visibility = Visibility.Collapsed;
                NoInternetGrid.Visibility = Visibility.Collapsed;
                LoadingProgressBar.IsIndeterminate = true;
                LoadingGrid.Visibility = Visibility.Visible;
            }
            else
            {
                SuraListView.Visibility = Visibility.Collapsed;
                NoInternetGrid.Visibility = Visibility.Visible;
                LoadingProgressBar.IsIndeterminate = false;
                LoadingGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void TryAgianBtn_Click(object sender, RoutedEventArgs e)
        {
            SuraListView.Visibility = Visibility.Collapsed;
            NoInternetGrid.Visibility = Visibility.Collapsed;
            LoadingProgressBar.IsIndeterminate = true;
            LoadingGrid.Visibility = Visibility.Visible;

            Task.Run(() => { QuranFunctionality.FetchSuraListFromServer(); });
            
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            QuranFunctionality.SuraListPopulatedChanged -= QuranFunctionality_SuraListPopulated;
        }

        private void SuraListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var surah = (Surah_vm)SuraListView.SelectedItem;
            Frame.Navigate(typeof(SurahViewPage), surah);
        }
    }
}
