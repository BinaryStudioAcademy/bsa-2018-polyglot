using App;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.DTO;
using Polyglot.BusinessLogic.Services;
using Polyglot.ViewModels.TranslationsDetails;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Polyglot.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {

        private IEnumerable<HistoryItemViewModel> _history;
        public IEnumerable<HistoryItemViewModel> History
        {
            get => _history;
            set => SetProperty(ref _history, value);
        }

        public async void Initialize(int complexStringId, string TranslationId)
        {

            var httpClient = new HttpClient();
            var token = UserService.Token;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = "http://polyglotbsa.azurewebsites.net/api/complexstrings/" + complexStringId + "/history/" + TranslationId+ "?itemsOnPage=20&page=0";

            var response = await httpClient.GetAsync(url);

            //will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            var history = JsonConvert.DeserializeObject<List<HistoryDTO>>(content);

            History = history.Select(x => new HistoryItemViewModel
            {
              Id=x.Id.ToString(),
              From=x.From,
              To=x.To,
              When=x.When
            }).ToList();


        }
    }
}
