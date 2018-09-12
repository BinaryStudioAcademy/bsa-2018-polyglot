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

namespace Polyglot.ViewModels.TranslationsDetails
{
    public class OptionalTranslationsViewModel : BaseViewModel
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

        private IEnumerable<OptionalTranslationViewModel> _optionalTranslations;
        public IEnumerable<OptionalTranslationViewModel> OptionalTranslations
        {
            get => _optionalTranslations;
            set
            {
                if (!SetProperty(ref _optionalTranslations, value))
                {
                    return;
                }
                RaisePropertyChanged(nameof(IsEmpty));
            }
        }

        public async void Initialize(int complexStringId, string translationId)
        {
            var url = "complexstrings/" + complexStringId + "/" + translationId + "/optional";
            var httpService = new HttpService();
            var optionalTranslations = await httpService.GetAsync<List<OptionalTranslationDTO>>(url);

            OptionalTranslations = optionalTranslations.Select(x => new OptionalTranslationViewModel
            {
                Translation = x.TranslationValue,
                UserName = x.UserName,
                DateTime = x.DateTime,
                UserPictureURL = x.UserPictureURL
            }).ToList();

            IsLoad = false;

            if (OptionalTranslations == null || !OptionalTranslations.Any())
            {
                _isEmpty = true;
            }
        }
    }
}
