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
using Windows.ApplicationModel.Core;

namespace Al_Quran.Models.CommunicationWithServer
{
    public class AlQuranCloudServer
    {
        public event EventHandler InternetError;
        public event EventHandler SurahPopulated;
        public event EventHandler JuzPopulated;
        public event EventHandler SurahTextTranslationFetched;


        public  async Task<(ObservableCollection<Surah_vm> surahs, bool internetError)> GetSuraListAsync()
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
                InternetError?.Invoke(this, new EventArgs());
                return (null, true);
            }
        }

        

        public  async Task<bool> GetJuzasync(int number, Juz_vm juz)
        {
            StringContent a = new StringContent("");
            string uri = $"http://api.alquran.cloud/v1/juz/{number}/quran-simple";

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
                                await CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => 
                                {
                                    Helpers.FetchDataHelper.GetJuz(quran.Juz, juz);
                                });
                                JuzPopulated?.Invoke(this, new EventArgs());
                                return false;
                            }
                            else return false;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                catch
                {
                    //cannot communicate with server. It may have many reasons.
                    return false;
                }
            }
            else
            {
                //ShowInternetErrorNotification();
                InternetError?.Invoke(this, new EventArgs());
                return true;
            }
        }


          


        public  async Task<bool> GetSurah(int surah_num, Surah_vm param_surah)
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

                                await CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {
                                    Helpers.FetchDataHelper.GetSurah(quranSurah.Data, param_surah);
                                });
                                SurahPopulated?.Invoke(this, new EventArgs());
                                return (false);

                            }
                            else return (false);
                        }
                        else
                        {
                            return (false);
                        }
                    }
                    catch(Exception ex)
                    {
                        return (false);
                    }
                }
                catch
                {
                    //cannot communicate with server. It may have many reasons.
                    return (false);
                }
            }
            else
            {
                //ShowInternetErrorNotification();
                InternetError?.Invoke(this, new EventArgs());
                return (true);
            }
        }






        public async Task<bool> GetJuzAyahTextTranslation(int number, Juz_vm juz, string identifier = "en.asad")
        {

            StringContent a = new StringContent("");
            string uri = $"http://api.alquran.cloud/v1/juz/{number}/{identifier}";

            if (CheckForInternetConnection())
            {
                try
                {
                    HttpResponseMessage response = await App.client.GetAsync(uri);

                    if(juz.CancelFetchingTranslation == true)
                    {
                        return false;
                    }

                    try
                    {
                        response.EnsureSuccessStatusCode();
                        // now serialize data
                        var stringContent = await response.Content.ReadAsStringAsync();

                        if (juz.CancelFetchingTranslation == true)
                        {
                            return false;
                        }

                        QuranJaz quran = QuranJaz.FromJson(stringContent);

                        if (quran.Status == "OK")
                        {
                            if (quran.Juz != null)
                            {
                                await CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {
                                    Helpers.FetchDataHelper.GetJuzAyahTextTranslation(quran.Juz, juz);
                                });
                                return false;
                            }
                            else return false;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                catch
                {
                    //cannot communicate with server. It may have many reasons.
                    return false;
                }
            }
            else
            {
                //ShowInternetErrorNotification();
                InternetError?.Invoke(this, new EventArgs());
                return true;
            }
        }






        public async Task<bool> GetSurahTextTranslation(int surah_num, Surah_vm param_surah, string identifier = "en.asad")
        {

            string uri = $"http://api.alquran.cloud/v1/surah/{surah_num}/{identifier}";

            if (CheckForInternetConnection())
            {
                try
                {
                    HttpResponseMessage response = await App.client.GetAsync(uri);

                    if(param_surah.CancelFetchingTranslation == true)
                    {
                        return false;
                    }

                    try
                    {
                        response.EnsureSuccessStatusCode();
                        // now serialize data
                        var stringContent = await response.Content.ReadAsStringAsync();

                        if (param_surah.CancelFetchingTranslation == true)
                        {
                            return false;
                        }

                        var quranSurah = QuranSurah.FromJson(stringContent);

                        if (quranSurah.Status == "OK")
                        {
                            if (quranSurah.Data != null)
                            {

                                await CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {
                                    Helpers.FetchDataHelper.GetSurahTextTranslation(quranSurah.Data, param_surah);
                                });
                                SurahTextTranslationFetched?.Invoke(this, new EventArgs());
                                return (false);

                            }
                            else return (false);
                        }
                        else
                        {
                            return (false);
                        }
                    }
                    catch (Exception ex)
                    {
                        return (false);
                    }
                }
                catch
                {
                    //cannot communicate with server. It may have many reasons.
                    return (false);
                }
            }
            else
            {
                //ShowInternetErrorNotification();
                InternetError?.Invoke(this, new EventArgs());
                return (true);
            }
        }





        public async Task<(bool internetError, List<Edition_vm> editonCollection)> GetEditionsAsync()
        {

            string uri = "http://api.alquran.cloud/v1/edition/format/text";

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
                        var editionData = EditionData.FromJson(stringContent);

                        if (editionData.Status == "OK")
                        {
                            if (editionData.Data != null)
                            {

                                var editions = Helpers.FetchDataHelper.GetEditions(editionData.Data);
                                
                                return (false, editions);

                            }
                            else return (false, null);
                        }
                        else
                        {
                            return (false, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        return (false, null);
                    }
                }
                catch
                {
                    //cannot communicate with server. It may have many reasons.
                    return (false, null);
                }
            }
            else
            {
                //ShowInternetErrorNotification();
                //InternetError?.Invoke(this, new EventArgs());
                return (true, null);
            }
        }




        public  bool CheckForInternetConnection()
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
