using Al_Quran.Models.API_Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Al_Quran.Models.CommunicationWithServer
{
    public class AlQuranCloudServer
    {

        public static async Task<(ObservableCollection<Surah_vm> surahs, bool internetError)> GetSuraListAsync()
        {
            StringContent a = new StringContent("");
            string uri = "http://api.alquran.cloud/v1/surah";

            if (CheckForInternetConnection())
            {
                try
                {
                    HttpResponseMessage response = await App.client.GetAsync(uri);
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        // now serialize data
                        var stringContent = await response.Content.ReadAsStringAsync();
                        Quran quran = JsonConvert.DeserializeObject<Quran>(stringContent);

                        if (quran.Status == "OK")
                        {
                            if (quran.Surahs != null)
                            {
                                var s = Helpers.FetchDataHelper.GetSuraList(quran.Surahs);
                                return (s, false);
                            }
                            else return (null, false);
                        }
                        else
                        {
                            return (null, false);
                        }
                    }
                    catch
                    {
                        return (null, false);
                    }
                }
                catch
                {
                    //cannot communicate with server. It may have many reasons.
                    return (null, false);
                }
            }
            else
            {
                //ShowInternetErrorNotification();
                return (null, true);
            }

            
        }



        public static async Task<Juz_vm> GetJuzsync(int number)
        {
            StringContent a = new StringContent("");
            string uri = $"http://api.alquran.cloud/v1/juz/{number}/en.asad";

            if (CheckForInternetConnection())
            {
                try
                {
                    HttpResponseMessage response = await App.client.GetAsync(uri);
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        // now serialize data
                        var stringContent = await response.Content.ReadAsStringAsync();
                        QuranJaz quran = QuranJaz.FromJson(stringContent);

                        if (quran.Status == "OK")
                        {
                            if (quran.Juz != null)
                            {
                                return Helpers.FetchDataHelper.GetJuz(quran.Juz);
                            }
                            else return null;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
                catch
                {
                    //cannot communicate with server. It may have many reasons.
                    return null;
                }
            }
            else
            {
                //ShowInternetErrorNotification();
                return null;
            }
        }





        public static async Task<(Surah_vm surah, bool internetError)> GetSurah(int surah_num)
        {
            
            string uri = $"http://api.alquran.cloud/v1/surah/{surah_num}";

            if (CheckForInternetConnection())
            {
                try
                {
                    HttpResponseMessage response = await App.client.GetAsync(uri);
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        // now serialize data
                        var stringContent = await response.Content.ReadAsStringAsync();
                        var quranSurah = QuranSurah.FromJson(stringContent);

                        if (quranSurah.Status == "OK")
                        {
                            if (quranSurah.Data != null)
                            {
                                var Surah = Helpers.FetchDataHelper.GetSurah(quranSurah.Data);
                                return (Surah, false);
                            }
                            else return (null, false);
                        }
                        else
                        {
                            return (null, false);
                        }
                    }
                    catch
                    {
                        return (null, false);
                    }
                }
                catch
                {
                    //cannot communicate with server. It may have many reasons.
                    return (null, false);
                }
            }
            else
            {
                //ShowInternetErrorNotification();
                return (null, true);
            }
        }





        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
