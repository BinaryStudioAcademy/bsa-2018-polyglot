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

namespace Polyglot.ViewModels.TranslationsDetails
{
    public class CommentVievModel : BaseViewModel
    {
        private const string DefaultImageUrl = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909__340.png";

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }


        private string _userPictureURL = DefaultImageUrl;
        public string UserPictureURL
        {
            get => _userPictureURL;
            set
            {
                if (_userPictureURL == value)
                {
                    return;
                }
                _userPictureURL = value.StartsWith("/") ? DefaultImageUrl : value;
                RaisePropertyChanged();
            }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set => SetProperty(ref _dateTime, value);
        }
    }
}
