using Newtonsoft.Json;
using Polyglot.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Implementations.HttpServices
{
    public class HttpService<TEntity, TIdentifyer> : IHttpService<TEntity, TIdentifyer>, IDisposable where TEntity : class
    {
        private bool disposed = false;
        private HttpClient httpClient;
        protected readonly string serviceBaseAddress;
        private readonly string addressSuffix;

        public HttpService(string serviceBaseAddress, string addresSufix)
        {
            this.serviceBaseAddress = serviceBaseAddress;
            this.addressSuffix = addresSufix;
            httpClient = MakeHttpClient(this.serviceBaseAddress);
        }

        protected virtual HttpClient MakeHttpClient(string serviceBaseAddress)
        {
            httpClient = new HttpClient();         
            httpClient.BaseAddress = new Uri(serviceBaseAddress);

            #region headers
            httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Poliglot", "1.0")));
            #endregion

            return httpClient;
        }

        public async Task DeleteAsync(TIdentifyer identifier)
        {
            var responseMessage = await httpClient.DeleteAsync(addressSuffix + identifier.ToString());
            if(responseMessage.IsSuccessStatusCode)
            {
                return;
            }
            throw new HttpRequestException(responseMessage.ReasonPhrase);
        }

        public async Task<TEntity> GetOneAsync(TIdentifyer identifier)
        {
            var responseMessage = await httpClient.GetAsync(addressSuffix + identifier.ToString());
            responseMessage.EnsureSuccessStatusCode();
            string json = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TEntity>(json);
            }
            throw new HttpRequestException(responseMessage.ReasonPhrase);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            var responseMessage = await httpClient.GetAsync(addressSuffix);
            responseMessage.EnsureSuccessStatusCode();
            string json = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(json);
            }
            throw new HttpRequestException(responseMessage.ReasonPhrase);
        }

        public async Task PutAsync(TIdentifyer identifier, TEntity entity)
        {
            var requestMessage = new HttpRequestMessage();
            string json = JsonConvert.SerializeObject(entity);
            var StringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync(addressSuffix + identifier.ToString(), StringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return;
            }
            throw new HttpRequestException(responseMessage.ReasonPhrase);
        }

        public async Task PostAsync(TEntity entity)
        {
            var requestMessage = new HttpRequestMessage();
            string json = JsonConvert.SerializeObject(entity);
            var StringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync(addressSuffix, StringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return;
            }
            throw new HttpRequestException(responseMessage.ReasonPhrase);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                if (httpClient != null)
                {
                    var hc = httpClient;
                    httpClient = null;
                    hc.Dispose();
                }
                disposed = true;
            }
        }
    }
}
