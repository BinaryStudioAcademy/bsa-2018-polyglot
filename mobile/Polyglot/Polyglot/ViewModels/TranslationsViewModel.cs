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

        public async void Initialize(int complexStringId, int projectId)
        {
            var httpClient = new HttpClient();
            var token = UserService.Token;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var translationsResponse = await httpClient.GetAsync("http://polyglotbsa.azurewebsites.net//api/complexstrings/" + complexStringId + "/translations");


            //will throw an exception if not successful
            translationsResponse.EnsureSuccessStatusCode();

            string content = await translationsResponse.Content.ReadAsStringAsync();

            var translations = JsonConvert.DeserializeObject<List<TranslationDTO>>(content);

            var languagesResponse = await httpClient.GetAsync("http://polyglotbsa.azurewebsites.net//api/projects/" + projectId + "/languages");


            //will throw an exception if not successful
            translationsResponse.EnsureSuccessStatusCode();

            string content2 = await translationsResponse.Content.ReadAsStringAsync();

            var languages = JsonConvert.DeserializeObject<List<LanguageDTO>>(content2);

            Translations = from lang in languages
                           join tr in translations
                           on lang.Id equals tr.LanguageId
                           select new TranslationViewModel
                           {
                               Language = lang.Name,
                               Translation = tr.TranslationValue == null ? "No Translation" : tr.TranslationValue
                           };
                
                
                /*languages.LeftJoin(translations, lang => lang.Id, tr => tr.LanguageId, (lang, tr) => new TranslationViewModel
            {
                Language = lang,
                Translation = tr
            }).ToList();*/
        }
    }
}
