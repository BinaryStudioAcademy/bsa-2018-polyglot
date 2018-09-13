using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using App;
using Newtonsoft.Json;
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

        public ProfileViewModel(UserDTO profile)
        {
            User = profile;
        }

        public async Task<List<TranslatorLanguageDTO>> GetUserLanguages(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.Token);
            var response = await httpClient.GetAsync("http://polyglotbsa.azurewebsites.net/api/languages/user/"+ userId);

            //will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            Languages = JsonConvert.DeserializeObject<List<TranslatorLanguageDTO>>(content);

            LanguagesLength = 50 * Languages.Count + 10;

            return Languages;
        }

        public async Task<List<RatingDTO>> GetUserReviews(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.Token);
            var response = await httpClient.GetAsync("http://polyglotbsa.azurewebsites.net/api/userprofiles/" + userId + "/ratings");

            //will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            Ratings = JsonConvert.DeserializeObject<List<RatingDTO>>(content);

            TranslatorRating = (int)Ratings.Select(x => x.Rate).ToList().Average();

            foreach (var rating in Ratings)
            {
                rating.User.AvatarUrl = rating.User.AvatarUrl == "/assets/images/default-avatar.jpg" ? "http://polyglotbsa.azurewebsites.net/assets/images/default-avatar.jpg" : rating.User.AvatarUrl;
            }

            ReviewsLength = 80 * Ratings.Count + 20;

            return Ratings;
        }
    }
}
