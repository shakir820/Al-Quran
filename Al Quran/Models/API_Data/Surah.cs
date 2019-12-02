using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Al_Quran.Models.API_Data
{
    public partial class QuranSurah
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public Surah Data { get; set; }
    }

    public partial class Surah
    {
        

        [JsonProperty("ayahs")]
        public List<Ayah> Ayahs { get; set; }

        [JsonProperty("edition")]
        public Edition Edition { get; set; }
    }
   

    public partial class QuranSurah
    {
        public static QuranSurah FromJson(string json) => JsonConvert.DeserializeObject<QuranSurah>(json, Converter.Settings);
    }

    
}
