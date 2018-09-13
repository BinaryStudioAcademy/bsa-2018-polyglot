using Polyglot.BusinessLogic;
using Polyglot.BusinessLogic.DTO;
using Polyglot.ViewModels;
using Polyglot.BusinessLogic.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditTranslationPage : ContentPage
	{
        public int ComplexStringId { get; set; }
        public TranslationViewModel Translation { get; set; }
        public UserDTO User { get; set; }

        public EditTranslationPage (int complexStringId,TranslationViewModel translation)
		{
            BindingContext = translation;
            ComplexStringId = complexStringId;
            Translation = translation;
            User = UserService.CurrentUser;

            InitializeComponent ();
		}

        private async void SaveTranslation_Clicked(object sender, EventArgs e)
        {
            var httpService = new HttpService();
            var translationsUrl = "complexstrings/" + ComplexStringId + "/translations";


            var editedTranslation = new TranslationDTO
            {
                Id = new Guid(Translation.Id),
                CreatedOn = DateTime.UtcNow,
                LanguageId =Translation.LanguageId,
                TranslationValue=Translation.Translation,
                UserId=User.Id
            };

            var translationResult = await httpService.PutAsync<TranslationDTO>(translationsUrl, editedTranslation);

            if (translationResult!=null)
            {
                await DisplayAlert("Result", "Translation saved!", "Ok");
                await Navigation.PopAsync();
            }
        }

    }
}