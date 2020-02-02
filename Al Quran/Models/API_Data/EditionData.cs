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
    public partial class EditionData
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<Edition> Data { get; set; }
    }

    public partial class Edition
    {
        

        [JsonProperty("direction")]
        public string Direction { get; set; }
    }

    public partial class EditionData
    {
        public static EditionData FromJson(string json) => JsonConvert.DeserializeObject<EditionData>(json, Converter.Settings);
    }
}
