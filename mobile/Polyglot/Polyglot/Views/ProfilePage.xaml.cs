using System.Threading.Tasks;
using Polyglot.BusinessLogic.DTO;
using Polyglot.BusinessLogic.Services;
using Polyglot.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
	    public ProfileViewModel _vm;
        public ProfilePage (ProfileViewModel vm,int userId)
        {
            _vm = vm;

		    BindingContext = _vm;

            _vm.LoadProfile(userId);

            InitializeComponent();

		    NavigationPage.SetHasNavigationBar(this, false);
		    Avatar.Source = _vm.User.AvatarUrl == "/assets/images/default-avatar.jpg" ? "http://polyglotbsa.azurewebsites.net/assets/images/default-avatar.jpg" : _vm.User.AvatarUrl;
		    Role.Text = _vm.User.UserRole == 1 ? "Manager" : "Tranlsator";

		    if (_vm.User.UserRole == 1)
		    {
		        Rating.IsVisible = false;
		        Reviews.IsVisible = false;
		        Languages.IsVisible = false;
		    }

		    _vm.GetUserLanguages(UserService.CurrentUser.Id);
		    _vm.GetUserReviews(UserService.CurrentUser.Id);

        }

	}
}