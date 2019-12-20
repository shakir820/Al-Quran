using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Al_Quran.Models
{

    [DataContract]
    public class JuzData
    {
        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public int BeginingAyahNo { get; set; }
        
        [DataMember]
        public int EndingAyahNo { get; set; }

        [DataMember]
        public List<string> Surahs = new List<string>();

        [DataMember]
        public int BeginningSurahAyahNo { get; set; }
        
        [DataMember]
        public int EndingSurahAyahNo { get; set; }
    }
}
