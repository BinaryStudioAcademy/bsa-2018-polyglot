using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.DTO;

namespace Polyglot.BusinessLogic.Services
{
    public static class UserService
    {
        public static string Token { get; set; }
        public static UserDTO CurrentUser { get; private set; }

        public static async Task<UserDTO> GetCurrentUserInstance()
        {
            if (CurrentUser == null)
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                var response = await httpClient.GetAsync("http://polyglotbsa.azurewebsites.net/api/userprofiles/user/");

                //will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<UserDTO>(content);

                CurrentUser = result;
            }

            return CurrentUser;
        }

        public static void Logout()
        {
            Token = String.Empty;
            CurrentUser = null;
        }

    }
}
