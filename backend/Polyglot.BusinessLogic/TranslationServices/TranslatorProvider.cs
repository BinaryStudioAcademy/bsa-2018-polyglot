using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(_url))
            using (HttpContent content = response.Content)
            {
                string responsJson = await content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return string.Empty;
                
            }
            return "";
        }

    }
}
