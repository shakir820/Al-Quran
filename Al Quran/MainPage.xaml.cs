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

        //private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{

        //    var grid = (Grid)sender;
        //    var TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(grid, 0);
        //    //var FontIconchild = (FontIcon)VisualTreeHelper.GetChild(grid, 0);

        //    //FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Bold;
        //    //TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Bold;
        //}



        //private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        //{
        //    var grid = (Grid)sender;
        //    var TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(grid, 1);
        //    var FontIconchild = (FontIcon)VisualTreeHelper.GetChild(grid, 0);
        //    if (_mainMenuNavigationItem != null)
        //    {
        //        if (_mainMenuNavigationItem.Name == TextBlockchild.Text)
        //        {
        //            return;
        //        }
        //    }
        //    TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
        //    FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
        //}

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

            //if (_mainMenuNavigationItem != null)
            //{
            //    _mainMenuNavigationItem.IsNavigated = false;
            //}

            //var item = (NavigationItem)NavigationListView.SelectedItem;
            //item.IsNavigated = true;
            //_mainMenuNavigationItem = item;

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

            UnPressedBottomBtns();
        }

        //private void SettingGrid_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{
        //    var grid = (Grid)sender;
        //    var TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(grid, 1);
        //    var FontIconchild = (FontIcon)VisualTreeHelper.GetChild(grid, 0);

        //    FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Bold;
        //    TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Bold;
        //}

        //private void SettingGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        //{
        //    var grid = (Grid)sender;
        //    var TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(grid, 1);
        //    var FontIconchild = (FontIcon)VisualTreeHelper.GetChild(grid, 0);

        //    if (BottomBtnsID == 1)
        //    {
        //        return;
        //    }

        //    FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
        //    TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
        //}

        //private void AboutGrid_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{
        //    var grid = (Grid)sender;
        //    var TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(grid, 1);
        //    var FontIconchild = (FontIcon)VisualTreeHelper.GetChild(grid, 0);

        //    FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Bold;
        //    TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Bold;
        //}

        //private void AboutGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        //{
        //    var grid = (Grid)sender;
        //    var TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(grid, 1);
        //    var FontIconchild = (FontIcon)VisualTreeHelper.GetChild(grid, 0);

        //    if (BottomBtnsID == 2)
        //    {
        //        return;
        //    }

        //    FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
        //    TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
        //}



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

            //grid = (Grid)SettingGrid;
            //TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(grid, 1);
            //FontIconchild = (FontIcon)VisualTreeHelper.GetChild(grid, 0);

            //FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
            //TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
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
            //settings btn
            //TextBlockchild = (TextBlock)VisualTreeHelper.GetChild(SettingGrid, 1);
            //FontIconchild = (FontIcon)VisualTreeHelper.GetChild(SettingGrid, 0);
            //FontIconchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
            //TextBlockchild.FontWeight = Windows.UI.Text.FontWeights.Normal;
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
           
        }
    }
}
