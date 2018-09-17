using App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.ViewModels.TranslationsDetails
{
    public class HistoryItemViewModel : BaseViewModel
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _from;
        public string From
        {
            get;
            set;
        }

        private string _to;
        public string To
        {
            get=>_to;
            set => SetProperty(ref _to, value);
        }

        private DateTime _when;
        public DateTime When
        {
            get=> _when;
            set => SetProperty(ref _when, value);
        }

    }
}
