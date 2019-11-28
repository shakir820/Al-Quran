using Al_Quran.Models.API_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Al_Quran.Models.Helpers
{
    public class FetchDataHelper
    {
        public static ObservableCollection<Surah_vm> GetSuraList(List<Surah> suraList)
        {
            ObservableCollection<Surah_vm> suraCollection = new ObservableCollection<Surah_vm>();

            foreach(var item in suraList)
            {
                var sura = new Surah_vm
                {
                    EnglishName = item.EnglishName,
                    EnglishNameTranslation = item.EnglishNameTranslation,
                    Name = item.Name,
                    Number = item.Number,
                    NumberOfAyahs = item.NumberOfAyahs,
                    RevelationType = item.RevelationType
                };
                suraCollection.Add(sura);
            }

            return suraCollection;
        }


        public static Juz_vm GetJuz(Juz juz)
        {

            var Juz = new Juz_vm();
            var edition = new Edition_vm();

            edition.EnglishName = juz.Edition.EnglishName;
            edition.Format = juz.Edition.Format;
            edition.Identifier = juz.Edition.Identifier;
            edition.Language = juz.Edition.Language;
            edition.Name = juz.Edition.Name;
            edition.Type = juz.Edition.Type;

            Juz.Edition = edition;

            Juz.Ayahs

            if(juz.Ayahs.Count > 0)
            {
                foreach(var ayah in juz.Ayahs)
                {
                    var Ayah = new Ayah_vm();
                    Ayah.HizbQuarter = ayah.HizbQuarter;
                    Ayah.Juz = ayah.Juz;
                    Ayah.Manzil = ayah.Manzil;
                    Ayah.Number = ayah.Number;
                    Ayah.NumberInSurah
                    
                }
            }


        }
    }
}
