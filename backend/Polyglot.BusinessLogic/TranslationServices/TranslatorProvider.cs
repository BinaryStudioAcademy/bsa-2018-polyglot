using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Polyglot.BusinessLogic.TranslationServices
{
    class TranslatorProvider : ITranslatorProvider
    {
        private readonly string _key;
        private readonly string _url;

        public TranslatorProvider(string providerKey,string providerUrl)
        {
            _key = providerKey;
            _url = providerUrl;
        }

        public async Task<string> Translate(TextForTranslation item)
        {
            HttpWebRequest httpRequest = WebRequest.CreateHttp(_url + "?key=" + _key);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
               await streamWriter.WriteAsync(JsonConvert.SerializeObject(item));
            }


            using (var response = (HttpWebResponse)await httpRequest.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                string responseFromServer = await reader.ReadToEndAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return string.Empty;

                return JObject.Parse(responseFromServer)["data"]["translations"].ToString();
            }

            //using (HttpClient client = new HttpClient())
            //using (HttpResponseMessage response = await client.GetAsync(_url))
            //using (HttpContent content = response.Content)
            //{
            //    string responsJson = await content.ReadAsStringAsync();
            //    if (response.StatusCode != HttpStatusCode.OK)
            //        return string.Empty;
            //    return JObject.Parse(responsJson)["data"]["translations"].ToString();
            //}
        }

    }
}
