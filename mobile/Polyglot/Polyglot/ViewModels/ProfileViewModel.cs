using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using App;
using Newtonsoft.Json;
using Polyglot.BusinessLogic;
using Polyglot.BusinessLogic.DTO;
using Polyglot.BusinessLogic.Services;

namespace Polyglot.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {

        private UserDTO _user;

        public UserDTO User
        {
            get => _user;
            set
            {
                if (!SetProperty(ref _user, value))
                {
                    return;
                }
            }
        }

        private List<TranslatorLanguageDTO> _languages;
        public List<TranslatorLanguageDTO> Languages
        {
            get => _languages;
            set => SetProperty(ref _languages, value);
        }

        private List<RatingDTO> _ratings;
        public List<RatingDTO> Ratings
        {
            get => _ratings;
            set => SetProperty(ref _ratings, value);
        }

        private int _languagesLength;
        public int LanguagesLength
        {
            get => _languagesLength;
            set => SetProperty(ref _languagesLength, value);
        }

        private int _reviewsLength;
        public int ReviewsLength
        {
            get => _reviewsLength;
            set => SetProperty(ref _reviewsLength, value);
        }

        private int _translatorRating;
        public int TranslatorRating
        {
            get => _translatorRating;
            set => SetProperty(ref _translatorRating, value);
        }

        public async Task LoadProfile(int userId)
        {
            if (userId != -1)
            {
                User = await HttpService.GetAsync<UserDTO>("userprofiles/" + userId);
            }
            else
            {
                User = UserService.CurrentUser;
            }
        }

        public async Task<List<TranslatorLanguageDTO>> GetUserLanguages(int userId)
        {
            Languages = await HttpService.GetAsync<List<TranslatorLanguageDTO>>("languages/user/" + userId);

            LanguagesLength = 50 * Languages.Count + 10;

            return Languages;
        }

        public async Task<List<RatingDTO>> GetUserReviews(int userId)
        {
            Ratings = await HttpService.GetAsync<List<RatingDTO>>("userprofiles/" + userId + "/ratings");

            foreach (var rating in Ratings)
            {
                rating.User.AvatarUrl = rating.User.AvatarUrl == "/assets/images/default-avatar.jpg" ? "http://polyglotbsa.azurewebsites.net/assets/images/default-avatar.jpg" : rating.User.AvatarUrl;
            }

            TranslatorRating = (int)Ratings.Select(x => x.Rate).ToList().Average();

            ReviewsLength = 80 * Ratings.Count;

            return Ratings;
        }
    }
}
