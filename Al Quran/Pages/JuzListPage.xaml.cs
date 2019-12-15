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
    public sealed partial class JuzListPage : Page, INotifyPropertyChanged
    {
        private QuranFunctionality quranFunctionality;


        public QuranFunctionality QuranFunctionality
        {
            get { return quranFunctionality; }
            set { quranFunctionality = value; RaisePropertyChanged(); }
        }



        public JuzListPage()
        {
            this.InitializeComponent();
            QuranFunctionality = QuranFunctionality.Current;
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
            MainPage.Current.HighlightMenuItem(typeof(JuzListPage));
        }
    }
}
