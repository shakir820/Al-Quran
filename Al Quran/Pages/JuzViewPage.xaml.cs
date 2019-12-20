using Al_Quran.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Al_Quran.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class JuzViewPage : Page, INotifyPropertyChanged
    {
        public JuzViewPage()
        {
            this.InitializeComponent();
            App.AlQuranCloudServer.InternetError += AlQuranCloudServer_InternetError;
            App.AlQuranCloudServer.JuzPopulated += AlQuranCloudServer_JuzPopulated; ;
        }

       

        private QuranFunctionality quranFunctionality;
        private Juz_vm _juz;

        public QuranFunctionality QuranFunctionality
        {
            get { return quranFunctionality; }
            set { quranFunctionality = value; RaisePropertyChanged(); }
        }

        public Juz_vm Juz
        {
            get { return _juz; }
            set { _juz = value; RaisePropertyChanged(); }
        }

      

        private async void AlQuranCloudServer_JuzPopulated(object sender, EventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                LoadingGrid.Visibility = Visibility.Collapsed;
                AyahListView.Visibility = Visibility.Visible;
                NoInternetGrid.Visibility = Visibility.Collapsed;
            });

            Juz.GetAyahTextTranslation();
        }

        private async void AlQuranCloudServer_InternetError(object sender, EventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                NoInternetGrid.Visibility = Visibility.Visible;
                AyahListView.Visibility = Visibility.Collapsed;
                LoadingGrid.Visibility = Visibility.Collapsed;
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var juz = (Juz_vm)e.Parameter;
            if (juz.Ayahs.Count == 0)
            {
                juz.GetAyah();
            }

            Juz = juz;
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
            MainPage.Current.HighlightMenuItem(typeof(JuzViewPage));
            if (Juz.Ayahs.Count == 0)
            {
                LoadingGrid.Visibility = Visibility.Visible;
                AyahListView.Visibility = Visibility.Collapsed;
                NoInternetGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoadingGrid.Visibility = Visibility.Collapsed;
                AyahListView.Visibility = Visibility.Visible;
                NoInternetGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void TryAgianBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadingGrid.Visibility = Visibility.Visible;
            AyahListView.Visibility = Visibility.Collapsed;
            NoInternetGrid.Visibility = Visibility.Collapsed;
            Juz.GetAyah();
        }
    }
}
