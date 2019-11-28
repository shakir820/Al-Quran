using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Al_Quran.Models
{
    public class Sajda_vm: INotifyPropertyChanged
    {
        private bool? _sajda_Existed;
        private long? _id;
        private bool? _recommended;
        private bool? _obligatory;


        public bool? Sajda_Existed
        {
            get { return _sajda_Existed; }
            set { _sajda_Existed = value; }
        }

        public long? Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(); }
        }

     
        public bool? Recommended
        {
            get { return _recommended; }
            set { _recommended = value; RaisePropertyChanged(); }
        }

      
        public bool? Obligatory
        {
            get { return _obligatory; }
            set { _obligatory = value; RaisePropertyChanged(); }
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
