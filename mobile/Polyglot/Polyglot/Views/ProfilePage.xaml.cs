using Polyglot.BusinessLogic.DTO;
using Polyglot.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
	    public ProfileViewModel _vm;
        public ProfilePage (UserDTO profile)
		{
		    _vm = new ProfileViewModel(profile);

		    BindingContext = _vm.User;
            InitializeComponent();

		    NavigationPage.SetHasNavigationBar(this, false);
		    Avatar.Source = _vm.User.AvatarUrl == "/assets/images/default-avatar.jpg" ? "http://polyglotbsa.azurewebsites.net/assets/images/default-avatar.jpg" : _vm.User.AvatarUrl;
		    Role.Text = _vm.User.UserRole == 1 ? "Manager" : "Tranlsator";
        }
	}
}