using App;
using Newtonsoft.Json;
using Polyglot.BusinessLogic;
using Polyglot.BusinessLogic.DTO;
using Polyglot.BusinessLogic.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Polyglot.ViewModels.TranslationsDetails
{
    public class CommentsViewModel : BaseViewModel
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

        private IEnumerable<CommentVievModel> _comments;
        public IEnumerable<CommentVievModel> Comments
        {
            get => _comments;
            set
            {
                if (!SetProperty(ref _comments, value))
                {
                    return;
                }

                RaisePropertyChanged(nameof(IsEmpty));

            }
        }

        public async void Initialize(int complexStringId)
        {
            var stringsUrl = "complexstrings/" + complexStringId + "/paginatedComments?itemsOnPage=50&page=0";
            var comments = await HttpService.GetAsync<List<CommentDTO>>(stringsUrl);

            Comments = comments.Select(x => new CommentVievModel
            {
                Text = x.Text,
                UserName = x.User.FullName,
                DateTime=x.CreatedOn,
                UserPictureURL=x.User.AvatarUrl
            }).ToList();

            IsLoad = false;

            if (Comments == null || !Comments.Any())
            {
                _isEmpty = true;
            }
        }
    }
}
