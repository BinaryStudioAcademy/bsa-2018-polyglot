using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Polyglot.ViewModels
{
    public class LoginViewModel
    {
        public async Task<string> LoginByEmail(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"email", email},
                    {"password", password},
                    {"returnSecureToken", "true"}

                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(
                    "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=AIzaSyDgXd8_8yIRLd5G5KWHmH60NlRl_qY6vGU",
                    content);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return string.Empty;
                }

                var result = await response.Content.ReadAsStringAsync();
                return JObject.Parse(result)["idToken"].ToString();
            }
        }
    }
}
