using CurrencyApiLib.Dtos.CurrencyRate;
using Newtonsoft.Json;

namespace CurrencyApi.HttpClient
{
    /// <summary>
    /// The web client.
    /// </summary>
    public class WebClient
    {
        public static CurrencyRateDto Request(string Url, string ApiKey,string currencyCode)
        {
            CurrencyRateDto data = new CurrencyRateDto();
            try
            {
                //TODO: Would of used RestSharp client
                string URLString = string.Format(Url, ApiKey, currencyCode.ToUpper());

                using (var webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(URLString);
                    data = JsonConvert.DeserializeObject<CurrencyRateDto>(json);

                    return data;
                }
            }
            catch (Exception ex)
            {
                //log
            }

            return data;
        }
    }
}
