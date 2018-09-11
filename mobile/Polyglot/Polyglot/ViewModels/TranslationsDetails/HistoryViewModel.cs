using App;
using Newtonsoft.Json;
using Polyglot.BusinessLogic;
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
        public bool IsEmpty => History == null || !History.Any();

        private IEnumerable<HistoryItemViewModel> _history;
        public IEnumerable<HistoryItemViewModel> History
        {
            get => _history;
            set
            {
                if (!SetProperty(ref _history, value))
                {
                    return;
                }

                RaisePropertyChanged(nameof(IsEmpty));
            }
        }

        public async void Initialize(int complexStringId, string TranslationId)
        {
            var url = "complexstrings/" + complexStringId + "/history/" + TranslationId+ "?itemsOnPage=20&page=0";
            var httpService = new HttpService();
            var history = await httpService.GetAsync<List<HistoryDTO>>(url);

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
