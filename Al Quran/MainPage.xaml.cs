using Al_Quran.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Al_Quran.Pages;
using System.Reflection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Al_Quran.Models.CommunicationWithServer;
using Windows.UI.Xaml.Media.Animation;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Windows.Storage;
using Al_Quran.Models.Events;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Al_Quran
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public Frame NavigationFrame;
        public static MainPage Current;
        public QuranFunctionality QuranFunctionality { get; set; }





        public event EventHandler<TranslationLanguageChangedEventArgs> TranslationLanguageChanged;








        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

            coreTitleBar.ExtendViewIntoTitleBar = true;
            var appTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            appTitleBar.ButtonBackgroundColor = Colors.Transparent;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            // Set XAML element as a draggable region.
            Window.Current.SetTitleBar(AppTitleBar);


            QuranFunctionality = new QuranFunctionality();


            NavigationItems.Add(new NavigationItem { Name = "Surah", IsNavigated = false, Type = typeof(SuraListPage) }); //Sura
            NavigationItems.Add(new NavigationItem { Name = "Juz", IsNavigated = false, Type = typeof(JuzListPage) }); //Juz


            
        }






        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            AppTitleBar.Height = sender.Height;
        }






        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationListView.SelectedIndex = 0;
            SelectFontSize();
            Task.Run(GetEditions);
        }






        private async void GetEditions()
        {
            PopulateLanguages();

            var result = await App.AlQuranCloudServer.GetEditionsAsync();
            if(result.internetError == true)
            {
                return;
            }

            if(result.editonCollection != null)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => 
                {
                    TranslationLanguageComboBox.ItemsSource = result.editonCollection;


                    var localSettings = ApplicationData.Current.LocalSettings;
                    if (localSettings.Values.ContainsKey("EditionIdentifier"))
                    {
                        var editionIdentity = (string) localSettings.Values["EditionIdentifier"];

                        if(result.editonCollection.Count > 0)
                        {
                            var SelectedEdition = result.editonCollection.SingleOrDefault(a => a.Identifier == editionIdentity);
                            if(SelectedEdition != null)
                            {
                                var selectedIndex = result.editonCollection.IndexOf(SelectedEdition);
                                TranslationLanguageComboBox.SelectedIndex = selectedIndex;
                                QuranFunctionality.SelectedTranslationIdentifier = editionIdentity;
                            }
                        }   
                    }
                });
            }
        }









        private void SelectFontSize()
        {
            switch (QuranFunctionality.FontSizeVM.FontSize)
            {
                case 16:
                    FontSizeComboBox.SelectedIndex = 0;
                    break;

                case 18:
                    FontSizeComboBox.SelectedIndex = 1;
                    break;

                case 20:
                    FontSizeComboBox.SelectedIndex = 2;
                    break;

                case 22:
                    FontSizeComboBox.SelectedIndex = 3;
                    break;

                case 24:
                    FontSizeComboBox.SelectedIndex = 4;
                    break;

                case 26:
                    FontSizeComboBox.SelectedIndex = 5;
                    break;

                case 28:
                    FontSizeComboBox.SelectedIndex = 6;
                    break;

                case 30:
                    FontSizeComboBox.SelectedIndex = 7;
                    break;

                default:
                    FontSizeComboBox.SelectedIndex = 0;
                    break;
            }
        }





        private void FontSizeVM_FontSizeChanged(object sender, EventArgs e)
        {
            
        }










        private void PopulateLanguages()
        {
            if(LanguageDictionary.Languages != null && LanguageDictionary.Languages.Count > 0)
            {
                return;
            }

            LanguageDictionary.Languages = new Dictionary<string, string>();
            LanguageDictionary.Languages.Add("az", "Azerbaijani");
            LanguageDictionary.Languages.Add("bn", "Bengali");
            LanguageDictionary.Languages.Add("cs", "Czech");
            LanguageDictionary.Languages.Add("de", "German");
            LanguageDictionary.Languages.Add("dv", "Divehi");
            LanguageDictionary.Languages.Add("en", "English");
            LanguageDictionary.Languages.Add("es", "Spanish");
            LanguageDictionary.Languages.Add("fa", "Farsi");
            LanguageDictionary.Languages.Add("fr", "French");
            LanguageDictionary.Languages.Add("ha", "Hausa");
            LanguageDictionary.Languages.Add("hi", "Hindi");
            LanguageDictionary.Languages.Add("id", "Indonesian");
            LanguageDictionary.Languages.Add("it", "Italian");
            LanguageDictionary.Languages.Add("ja", "Japanese");
            LanguageDictionary.Languages.Add("ko", "Korean");
            LanguageDictionary.Languages.Add("ku", "Kurdish");
            LanguageDictionary.Languages.Add("ml", "Malayalam");
            LanguageDictionary.Languages.Add("nl", "Dutch");
            LanguageDictionary.Languages.Add("no", "Norwegian");
            LanguageDictionary.Languages.Add("pl", "Polish");
            LanguageDictionary.Languages.Add("pt", "Portuguese");
            LanguageDictionary.Languages.Add("ro", "Romanian");
            LanguageDictionary.Languages.Add("ru", "Russian");
            LanguageDictionary.Languages.Add("sd", "Sindhi");
            LanguageDictionary.Languages.Add("so", "Somali");
            LanguageDictionary.Languages.Add("sq", "Albanian");
            LanguageDictionary.Languages.Add("sv", "Swedish");
            LanguageDictionary.Languages.Add("sw", "Swahili");
            LanguageDictionary.Languages.Add("ta", "Tamil");
            LanguageDictionary.Languages.Add("tg", "Tajik");
            LanguageDictionary.Languages.Add("th", "Thai");
            LanguageDictionary.Languages.Add("tr", "Turkish");
            LanguageDictionary.Languages.Add("tt", "Tatar");
            LanguageDictionary.Languages.Add("ug", "Uighur");
            LanguageDictionary.Languages.Add("ur", "Urdu");
            LanguageDictionary.Languages.Add("uz", "Uzbek");
        }











        #region navigation
        private NavigationItem _mainMenuNavigationItem;
        private int BottomBtnsID = 0;

        private ObservableCollection<NavigationItem> _navigationItems = new ObservableCollection<NavigationItem>();

        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; RaisePropertyChanged(); }
        }


        public TextBlock NavigatedTextBlockItem;

      






        internal static void FindChildren<T>(List<T> results, DependencyObject startNode) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                FindChildren<T>(results, current);
            }
        }










        private void NavigationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavigationListView.SelectedIndex == -1)
            {
                return;
            }

          
            switch (NavigationListView.SelectedIndex)
            {
                case 0:
                    frame.Navigate(typeof(SuraListPage), null, new DrillInNavigationTransitionInfo());
                    break;
                case 1:
                    frame.Navigate(typeof(JuzListPage), null, new DrillInNavigationTransitionInfo());
                    break;
                
                default:
                    frame.Navigate(typeof(SuraListPage), null, new DrillInNavigationTransitionInfo());
                    break;
            }


           // UnPressedBottomBtns();
        }











        public void HighlightMenuItem(Type sourcePageType)
        {
            if(_mainMenuNavigationItem != null)
            {
                _mainMenuNavigationItem.IsNavigated = false;
            }

            if(sourcePageType == typeof(SurahViewPage))
            {
                var item = NavigationItems.SingleOrDefault(a => a.Type == typeof(SuraListPage));
                
                if(item != null)
                {
                    _mainMenuNavigationItem = item;
                    item.IsNavigated = true;
                }
            }
            else if(sourcePageType == typeof(SuraListPage))
            {
                var item = NavigationItems.SingleOrDefault(a => a.Type == typeof(SuraListPage));

                if (item != null)
                {
                    _mainMenuNavigationItem = item;
                    item.IsNavigated = true;
                }
            }
            else if (sourcePageType == typeof(JuzListPage))
            {
                var item = NavigationItems.SingleOrDefault(a => a.Type == typeof(JuzListPage));

                if (item != null)
                {
                    _mainMenuNavigationItem = item;
                    item.IsNavigated = true;
                }
            }
            else if(sourcePageType == typeof(JuzViewPage))
            {
                var item = NavigationItems.SingleOrDefault(a => a.Type == typeof(JuzListPage));

                if (item != null)
                {
                    _mainMenuNavigationItem = item;
                    item.IsNavigated = true;
                }
            }

            UnPressedBottomBtns();
        }

      








        public void HightLightAboutButton()
        {
            var grid = AboutGrid;
            var TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(grid, 1);
            var FontIconchild = (FontIcon)VisualTreeHelper.GetChild(grid, 0);

            FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Bold;
            TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Bold;

            BottomBtnsID = 2;
            if (_mainMenuNavigationItem != null)
            {
                _mainMenuNavigationItem.IsNavigated = false;
                //_mainMenuNavigationItem = null;
            }
            NavigationListView.SelectedIndex = -1;

          
        }








        public void HightLightSettingButton()
        {
            var grid = SettingGrid;
            var TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(grid, 1);
            var FontIconchild = (FontIcon)VisualTreeHelper.GetChild(grid, 0);

            FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Bold;
            TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Bold;

            BottomBtnsID = 1;
            if (_mainMenuNavigationItem != null)
            {
                _mainMenuNavigationItem.IsNavigated = false;
                //_mainMenuNavigationItem = null;
            }
            NavigationListView.SelectedIndex = -1;

            grid = (Grid)AboutGrid;
            TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(grid, 1);
            FontIconchild = (FontIcon)VisualTreeHelper.GetChild(grid, 0);

            FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
            TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
        }









        private void AboutGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
           
            frame.Navigate(typeof(AboutPage));
        }









        private void SettingGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
          
            frame.Navigate(typeof(SettingPage));
        }










        private void UnPressedBottomBtns()
        {
            //about btn
            BottomBtnsID = 0;
            var TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(AboutGrid, 1);
            var FontIconchild = (FontIcon)VisualTreeHelper.GetChild(AboutGrid, 0);
            FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
            TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
           
        }
        #endregion










        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName]string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }











        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }













        private void frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (frame.CanGoBack)
            {
                BackButton.Visibility = Visibility.Visible;
            }
            else
            {
                BackButton.Visibility = Visibility.Collapsed;
            }
           
            if(e.SourcePageType == typeof(SurahViewPage) || e.SourcePageType == typeof(JuzViewPage))
            {
                ShowOptionPanel();
            }
            else
            {


                if(OptionStackPanel.Visibility == Visibility.Visible)
                {
                    HideOptionPanel();
                }
            }

            NavigationListView.SelectedIndex = -1;
        }











        public async void ShowOptionPanel()
        {
            OptionStackPanel.Visibility = Visibility.Visible;
            _= OptionStackPanel.Offset(offsetX: 0.0f, offsetY: -50.0f, duration: 300, delay: 200).StartAsync();
            await OptionStackPanel.Fade(value: 1.0f, duration: 300, delay: 200).StartAsync();

        }










        public async void HideOptionPanel()
        {
          
            _ = OptionStackPanel.Fade(value: 0.0f, duration: 300, delay: 0).StartAsync();
            await OptionStackPanel.Offset(offsetX: 0.0f, offsetY: 50.0f, duration: 300, delay: 0).StartAsync();
            OptionStackPanel.Visibility = Visibility.Collapsed;
        }











        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            switch (FontSizeComboBox.SelectedIndex)
            {
                case 0:
                    QuranFunctionality.FontSizeVM.FontSize = 16;
                    break;

                case 1:
                    QuranFunctionality.FontSizeVM.FontSize = 18;
                    break;

                case 2:
                    QuranFunctionality.FontSizeVM.FontSize = 20;
                    break;

                case 3:
                    QuranFunctionality.FontSizeVM.FontSize = 22;
                    break;

                case 4:
                    QuranFunctionality.FontSizeVM.FontSize = 24;
                    break;

                case 5:
                    QuranFunctionality.FontSizeVM.FontSize = 26;
                    break;

                case 6:
                    QuranFunctionality.FontSizeVM.FontSize = 28;
                    break;

                case 7:
                    QuranFunctionality.FontSizeVM.FontSize = 30;
                    break;
            }

        }












        private void TranslationLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TranslationLanguageComboBox.SelectedIndex == -1)
            {
                return;
            }

            var selectedItem = (Edition_vm) TranslationLanguageComboBox.SelectedItem;

            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["EditionIdentifier"] = selectedItem.Identifier;

            QuranFunctionality.SelectedTranslationIdentifier = selectedItem.Identifier;

            TranslationLanguageChanged?.Invoke(this, new TranslationLanguageChangedEventArgs { Identifier = selectedItem.Identifier });

        }

        private void autoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
             if(QuranFunctionality.SuraCollection.Count > 0)
            {
                var searchedSurah = new List<Surah_vm>();

                searchedSurah = QuranFunctionality.SuraCollection.Where(a => a.EnglishName.IndexOf(args.QueryText, StringComparison.CurrentCultureIgnoreCase) > -1).ToList();

                frame.Navigate(typeof(SearchPage), searchedSurah);
            }
        }
    }


    
}
