using Newtonsoft.Json;
using Polyglot.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic
{
    public class HttpService
    {
        public async Task<TResult> GetAsync<TResult>(string relativeUrl)
        {
            using(  var httpClient = new HttpClient())
            {
                var token = UserService.Token;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var stringsUrl = "http://polyglotbsa.azurewebsites.net/api/" + relativeUrl;
                var response = await httpClient.GetAsync(stringsUrl);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TResult>(content);

                return result;
            }


            

        }
    }
}
