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

    public partial class QuranJaz
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public Juz Juz { get; set; }
    }

    public partial class Juz
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("ayahs")]
        public List<Ayah> Ayahs { get; set; }

        [JsonProperty("surahs")]
        public Dictionary<string, Surah> Surahs { get; set; }

        [JsonProperty("edition")]
        public Edition Edition { get; set; }
    }

    public partial class Ayah
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("surah")]
        public Surah Surah { get; set; }

        [JsonProperty("numberInSurah")]
        public long NumberInSurah { get; set; }

        [JsonProperty("juz")]
        public long Juz { get; set; }

        [JsonProperty("manzil")]
        public long Manzil { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("ruku")]
        public long Ruku { get; set; }

        [JsonProperty("hizbQuarter")]
        public long HizbQuarter { get; set; }

        [JsonProperty("sajda")]
        public SajdaUnion Sajda { get; set; }
        
    }

    public partial class SajdaClass
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("recommended")]
        public bool Recommended { get; set; }

        [JsonProperty("obligatory")]
        public bool Obligatory { get; set; }
    }

   
    public partial class Edition
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("englishName")]
        public string EnglishName { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial struct SajdaUnion
    {
        public bool? Bool;
        public SajdaClass SajdaClass;

        public static implicit operator SajdaUnion(bool Bool) => new SajdaUnion { Bool = Bool };
        public static implicit operator SajdaUnion(SajdaClass SajdaClass) => new SajdaUnion { SajdaClass = SajdaClass };
    }

    public partial class QuranJaz
    {
        public static QuranJaz FromJson(string json) => JsonConvert.DeserializeObject<QuranJaz>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this QuranJaz self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                SajdaUnionConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class SajdaUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SajdaUnion) || t == typeof(SajdaUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Boolean:
                    var boolValue = serializer.Deserialize<bool>(reader);
                    return new SajdaUnion { Bool = boolValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<SajdaClass>(reader);
                    return new SajdaUnion { SajdaClass = objectValue };
            }
            throw new Exception("Cannot unmarshal type SajdaUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (SajdaUnion)untypedValue;
            if (value.Bool != null)
            {
                serializer.Serialize(writer, value.Bool.Value);
                return;
            }
            if (value.SajdaClass != null)
            {
                serializer.Serialize(writer, value.SajdaClass);
                return;
            }
            throw new Exception("Cannot marshal type SajdaUnion");
        }

        public static readonly SajdaUnionConverter Singleton = new SajdaUnionConverter();
    }
}
