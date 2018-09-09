using App;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.DTO;
using Polyglot.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Polyglot.ViewModels
{
    public class TranslationsViewModel : BaseViewModel
    {
        private IEnumerable<TranslationViewModel> _translations;
        public IEnumerable<TranslationViewModel> Translations
        {
            get => _translations;
            set => SetProperty(ref _translations, value);
        }

        public async void Initialize(string complexStringId)
        {
            var httpClient = new HttpClient();
            var token = UserService.Token;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("http://polyglotbsa.azurewebsites.net//api/complexstrings/" + complexStringId + "/translations");


            //will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            var translations = JsonConvert.DeserializeObject<List<TranslationDTO>>(content);

        }
    }
}
