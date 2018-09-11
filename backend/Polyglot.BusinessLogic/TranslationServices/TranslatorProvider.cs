using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Polyglot.BusinessLogic.TranslationServices
{
    public class TranslatorProvider : ITranslatorProvider
    {
        private readonly string _url;
        private readonly string _key;

        public TranslatorProvider(string providerUrl,string providerKey)
        {
            _url = providerUrl;
            _key = providerKey;
        }

        public async Task<string> Translate(TextForTranslation item)
        {
            var path = _url + "?key=" + _key;

            HttpWebRequest httpRequest = WebRequest.CreateHttp(path);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";

            byte[] byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));

            httpRequest.ContentLength = byteArray.Length;

            using (var streamWriter = httpRequest.GetRequestStream())
            {
                await streamWriter.WriteAsync(byteArray, 0, byteArray.Length);
            }

            try
            {
                using (var response = (HttpWebResponse) await httpRequest.GetResponseAsync())

                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string responseFromServer = await reader.ReadToEndAsync();

                    if (response.StatusCode != HttpStatusCode.OK)
                        return string.Empty;

                    return JObject.Parse(responseFromServer)["data"]["translations"].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
