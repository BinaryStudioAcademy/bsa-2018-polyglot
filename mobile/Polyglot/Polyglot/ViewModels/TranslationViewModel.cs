using App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.ViewModels
{
    public class TranslationViewModel : BaseViewModel
    {
        private string _language;
        public string Language
        {
            get => _language;
            set => SetProperty(ref _language, value);
        }

        private string _translation;
        public string Translation
        {
            get => _translation;
            set => SetProperty(ref _translation, value);
        }

    }
}
