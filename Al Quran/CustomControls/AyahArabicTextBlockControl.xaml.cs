using Al_Quran.Models;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Al_Quran.CustomControls
{
    public sealed partial class AyahArabicTextBlockControl : UserControl
    {
        public AyahArabicTextBlockControl()
        {
            this.InitializeComponent();
        }



        public FontSizeVM FontSizeVM = FontSizeVM.Current;


        public string ArabicText
        {
            get { return (string)GetValue(ArabicTextProperty); }
            set { SetValue(ArabicTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArabicText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArabicTextProperty =
            DependencyProperty.Register("ArabicText", typeof(string), typeof(AyahArabicTextBlockControl), new PropertyMetadata(null, ArabicTextChangedCallback));

        private static void ArabicTextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (AyahArabicTextBlockControl)d;
            control.textBlock.Text = (string) e.NewValue;
        }
    }
}
