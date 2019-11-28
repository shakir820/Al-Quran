using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Al_Quran.Models
{
    public class Edition_vm: INotifyPropertyChanged
    {
        private string _identifier;
        private string _language;
        private string _name;
        private string _englishName;
        private string _format;
        private string _type;



        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value; RaisePropertyChanged(); }
        }
        public string Language
        {
            get { return _language; }
            set { _language = value; RaisePropertyChanged(); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }
        public string EnglishName
        {
            get { return _englishName; }
            set { _englishName = value; RaisePropertyChanged(); }
        }
        public string Format
        {
            get { return _format; }
            set { _format = value; RaisePropertyChanged(); }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; RaisePropertyChanged(); }
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
