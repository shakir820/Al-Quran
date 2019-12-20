using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Al_Quran.Models
{
    [DataContract]
    public class JuzDataCollection
    {
        [DataMember]
        public List<JuzData> JuzCollection = new List<JuzData>();
    }
}
