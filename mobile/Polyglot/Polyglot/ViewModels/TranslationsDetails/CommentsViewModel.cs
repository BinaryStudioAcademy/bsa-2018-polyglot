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
        public bool IsEmpty => Comments == null || !Comments.Any();


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
            var stringsUrl = "complexstrings/" + complexStringId + "/paginatedComments?itemsOnPage=20&page=0";
            var httpService = new HttpService();
            var comments = await httpService.GetAsync<List<CommentDTO>>(stringsUrl);

            Comments = comments.Select(x => new CommentVievModel
            {
                Text = x.Text,
                UserName = x.User.FullName,
                DateTime=x.CreatedOn,
                UserPictureURL=x.User.AvatarUrl
            }).ToList();

        }
    }
}
