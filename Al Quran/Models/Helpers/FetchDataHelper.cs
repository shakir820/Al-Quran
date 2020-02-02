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


        public static void GetJuz(Juz juz, Juz_vm juz_vm)
        {

           
            var edition = new Edition_vm();

            edition.EnglishName = juz.Edition.EnglishName;
            edition.Format = juz.Edition.Format;
            edition.Identifier = juz.Edition.Identifier;
            edition.Language = juz.Edition.Language;
            edition.Name = juz.Edition.Name;
            edition.Type = juz.Edition.Type;

            juz_vm.Edition = edition;

            

            if(juz.Ayahs.Count > 0)
            {
                foreach(var ayah in juz.Ayahs)
                {
                    var Ayah = new Ayah_vm();
                    Ayah.HizbQuarter = ayah.HizbQuarter;
                    Ayah.Juz = ayah.Juz;
                    Ayah.Manzil = ayah.Manzil;
                    Ayah.Number = ayah.Number;
                    Ayah.NumberInSurah = ayah.NumberInSurah;
                    Ayah.Page = ayah.Page;
                    Ayah.Ruku = ayah.Ruku;


                    Ayah.Sajda = new  Sajda_vm();

                    if(ayah.Sajda.Bool != null)
                    {
                        Ayah.Sajda.Sajda_Existed = false;
                    }
                    else
                    {
                        Ayah.Sajda.Id = ayah.Sajda.SajdaClass.Id;
                        Ayah.Sajda.Obligatory = ayah.Sajda.SajdaClass.Obligatory;
                        Ayah.Sajda.Recommended = ayah.Sajda.SajdaClass.Recommended;
                    }

                    Ayah.Surah = new Surah_vm();
                    Ayah.Surah.EnglishName = ayah.Surah.EnglishName;
                    Ayah.Surah.EnglishNameTranslation = ayah.Surah.EnglishNameTranslation;
                    Ayah.Surah.Name = ayah.Surah.Name;
                    Ayah.Surah.Number = ayah.Surah.Number;
                    Ayah.Surah.NumberOfAyahs = ayah.Surah.NumberOfAyahs;
                    Ayah.Surah.RevelationType = ayah.Surah.RevelationType;
                    Ayah.Text = ayah.Text;

                    juz_vm.Ayahs.Add(Ayah);
                }
            }

            juz_vm.Number = juz.Number;
            juz_vm.Surahs = new Dictionary<string, Surah_vm>();

            foreach(var item in juz.Surahs)
            {
                var surah = new Surah_vm
                {
                    EnglishName = item.Value.EnglishName,
                    EnglishNameTranslation = item.Value.EnglishNameTranslation,
                    Name = item.Value.Name,
                    Number = item.Value.Number,
                    NumberOfAyahs = item.Value.NumberOfAyahs,
                    RevelationType = item.Value.RevelationType
                };

                juz_vm.Surahs.Add(item.Key, surah);
            }
        }



        public static void GetSurah(Surah surah, Surah_vm Surah)
        {
            
            if(surah.Ayahs != null)
            {
                if(surah.Ayahs.Count > 0)
                {
                    foreach(var ayah_item in surah.Ayahs)
                    {
                        var Ayah = new Ayah_vm();
                        Ayah.HizbQuarter = ayah_item.HizbQuarter;
                        Ayah.Juz = ayah_item.Juz;
                        Ayah.Manzil = ayah_item.Manzil;
                        Ayah.Number = ayah_item.Number;
                        Ayah.NumberInSurah = ayah_item.NumberInSurah;
                        Ayah.Page = ayah_item.Page;
                        Ayah.Ruku = ayah_item.Ruku;
                        Ayah.Sajda = new Sajda_vm();
                        if(ayah_item.Sajda.Bool != null)
                        {
                            Ayah.Sajda.Sajda_Existed = false;
                        }
                        else
                        {
                            Ayah.Sajda.Id = ayah_item.Sajda.SajdaClass.Id;
                            Ayah.Sajda.Obligatory = ayah_item.Sajda.SajdaClass.Obligatory;
                            Ayah.Sajda.Recommended = ayah_item.Sajda.SajdaClass.Recommended;
                        }

                        Ayah.Surah = Surah;
                        Ayah.Text = ayah_item.Text;
                        Surah.Ayahs.Add(Ayah);
                    }
                }
            }

            Surah.Edition = new Edition_vm();
            Surah.Edition.EnglishName = surah.Edition.EnglishName;
            Surah.Edition.Format = surah.Edition.Format;
            Surah.Edition.Identifier = surah.Edition.Identifier;
            Surah.Edition.Language = surah.Edition.Language;
            Surah.Edition.Name = surah.Edition.Name;
            Surah.Edition.Type = surah.Edition.Type;
            Surah.EnglishName = surah.EnglishName;
            Surah.EnglishNameTranslation = surah.EnglishNameTranslation;
            Surah.Name = surah.Name;
            Surah.Number = surah.Number;
            Surah.NumberOfAyahs = surah.NumberOfAyahs;
            Surah.RevelationType = surah.RevelationType;

            //return Surah;
        }


        public static void GetSurahTextTranslation(Surah surah, Surah_vm Surah)
        {

            if (surah.Ayahs != null)
            {
                if (surah.Ayahs.Count > 0)
                {
                    //int index = 1;

                    for(var i = 0; i < Surah.Ayahs.Count; i++)
                    {
                        Surah.Ayahs[i].TranslationText = surah.Ayahs[i].Text;
                    }
                }
            }

            

            //return Surah;
        }

        public static void GetJuzAyahTextTranslation(Juz juz1, Juz_vm juz2)
        {
            if (juz1.Ayahs != null)
            {
                if (juz1.Ayahs.Count > 0)
                {
                    for (var i = 0; i < juz1.Ayahs.Count; i++)
                    {
                        juz2.Ayahs[i].TranslationText = juz1.Ayahs[i].Text;
                    }
                }
            }
        }

        public static List<Edition_vm> GetEditions(List<Edition> data)
        {
            var editionCollection = new List<Edition_vm>();

            var filteredCollection = data.Where(a => a.Format == "text" && a.Type == "translation").ToList();

            foreach(var edition_item in filteredCollection)
            {
                var edition = new Edition_vm();
                edition.EnglishName = edition_item.EnglishName;
                edition.Format = edition_item.Format;
                edition.Identifier = edition_item.Identifier;

                bool LanguageExisted = false;

                if(LanguageDictionary.Languages.ContainsKey(edition_item.Language))
                {
                    LanguageExisted = true;
                    var language = LanguageDictionary.Languages[edition_item.Language];
                    edition.Language = language;
                }

                edition.Name = edition_item.Name;
                edition.Type = edition_item.Type;

                if (LanguageExisted == true)
                    editionCollection.Add(edition);
            }


            return editionCollection;


        }
    }
}
