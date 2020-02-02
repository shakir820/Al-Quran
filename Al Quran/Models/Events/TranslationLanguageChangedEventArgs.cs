using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Al_Quran.Models.Events
{
    public class TranslationLanguageChangedEventArgs: EventArgs
    {
        public string Identifier { get; set; }
    }
}
