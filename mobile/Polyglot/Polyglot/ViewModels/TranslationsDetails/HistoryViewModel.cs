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
        private const bool DefaultIsEmpty = false;
        private const bool DefaultIsLoad = true;


        private bool _isEmpty = DefaultIsEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                SetProperty(ref _isEmpty, value);
            }
        }

        private bool _isLoad = DefaultIsLoad;
        public bool IsLoad
        {
            get => _isLoad;
            set
            {
                SetProperty(ref _isLoad, value);
            }
        }

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
            var url = "complexstrings/" + complexStringId + "/history/" + TranslationId+ "?itemsOnPage=50&page=0";
            var history = await HttpService.GetAsync<List<HistoryDTO>>(url);

            History = history.Select(x => new HistoryItemViewModel
            {
              Id=x.Id.ToString(),
              From=x.From,
              To=x.To,
              When=x.When
            }).ToList();

            IsLoad = false;

            if (History == null || !History.Any())
            {
                _isEmpty = true;
            }
        }
    }
}
