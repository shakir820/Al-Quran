﻿using Al_Quran.Models;
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
    public sealed partial class SurahViewPage : Page, INotifyPropertyChanged
    {

        private QuranFunctionality quranFunctionality;
        private Surah_vm _surah;

        public QuranFunctionality QuranFunctionality
        {
            get { return quranFunctionality; }
            set { quranFunctionality = value; RaisePropertyChanged(); }
        }

        public Surah_vm Surah
        {
            get { return _surah; }
            set { _surah = value; RaisePropertyChanged(); }
        }

        public SurahViewPage()
        {
            this.InitializeComponent();
            App.AlQuranCloudServer.InternetError += AlQuranCloudServer_InternetError;
            App.AlQuranCloudServer.SurahPopulated += AlQuranCloudServer_SurahPopulated;
        }

        private async void AlQuranCloudServer_SurahPopulated(object sender, EventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => 
            {
                LoadingGrid.Visibility = Visibility.Collapsed;
                AyahListView.Visibility = Visibility.Visible;
                NoInternetGrid.Visibility = Visibility.Collapsed;
            });
            
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
            var surah = (Surah_vm)e.Parameter;
            surah.GetAyah();
            Surah = surah;
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
            if(Surah.Ayahs.Count == 0)
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
            Surah.GetAyah();
        }
    }
}
