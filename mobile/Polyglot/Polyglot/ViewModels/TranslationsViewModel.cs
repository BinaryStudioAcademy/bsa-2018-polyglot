using App;
using Newtonsoft.Json;
using Polyglot.BusinessLogic;
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
        private const bool DefaultIsLoad = true;

        private bool _isLoad = DefaultIsLoad;
        public bool IsLoad
        {
            get => _isLoad;
            set
            {
                SetProperty(ref _isLoad, value);
            }
        }

        private IEnumerable<TranslationViewModel> _translations;
        public IEnumerable<TranslationViewModel> Translations
        {
            get => _translations;
            set {
                if (!SetProperty(ref _translations, value))
                {
                    return;
                }

                RaisePropertyChanged(nameof(IsLoad));
            }
        }

        public async void Initialize(int complexStringId, int projectId)
        {
            var httpService = new HttpService();

            var translationsUrl = "complexstrings/" + complexStringId + "/translations";
            var translations = await httpService.GetAsync<List<TranslationDTO>>(translationsUrl);

            var langUrl = "projects/" + projectId + "/languages/";
            var languages = await httpService.GetAsync<List<LanguageDTO>>(langUrl);

            Translations = from lang in languages
                           join tr in translations
                           on lang.Id equals tr.LanguageId
                           into val
                           from t in val.DefaultIfEmpty()
                           select new TranslationViewModel
                           {
                               Id= t == null ? "" : t.Id.ToString(),
                               Language = lang.Name,
                               Translation = t == null ? "Not translated" : t.TranslationValue
                           };

            Translations = Translations.ToList();

            IsLoad = false;
        }
    }
}
