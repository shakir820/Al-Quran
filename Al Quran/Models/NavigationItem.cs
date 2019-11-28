using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Al_Quran.Models
{
    public class NavigationItem: INotifyPropertyChanged
    {
        private string _name;
        private bool _isNavigated;
        private string _icon;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }


        public Type Type { get; set; }

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; RaisePropertyChanged(); }
        }



        public bool IsNavigated
        {
            get { return _isNavigated; }
            set { _isNavigated = value; RaisePropertyChanged(); }
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
