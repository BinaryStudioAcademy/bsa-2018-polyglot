using App;
using Polyglot.BusinessLogic;
using Polyglot.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.ViewModels
{
    public class TranslationViewModel : BaseViewModel
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _language;
        public string Language
        {
            get => _language;
            set => SetProperty(ref _language, value);
        }

        private int _languageId;
        public int LanguageId
        {
            get => _languageId;
            set => SetProperty(ref _languageId, value);
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private DateTime _createdOn;
        public DateTime CreatedOn
        {
            get => _createdOn;
            set => SetProperty(ref _createdOn, value);
        }

        private string _translation;
        public string Translation
        {
            get => _translation;
            set => SetProperty(ref _translation, value);
        }

    }
}
