using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}
