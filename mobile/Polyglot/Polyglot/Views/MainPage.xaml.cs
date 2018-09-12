using System;
using Polyglot.BusinessLogic.DTO;
using Polyglot.BusinessLogic.Services;
using Polyglot.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : MasterDetailPage
	{
        public UserDTO User { get; set; }

		public MainPage()
		{
			InitializeComponent();
		    NavigationPage.SetHasNavigationBar(this, false);
		    IsPresented = false;

            Detail = new NavigationPage(new DashboardPage(new DashboardViewModel()));

		    User = UserService.CurrentUser;

            Avatar.Source = User.AvatarUrl == "/assets/images/default-avatar.jpg" ? "http://polyglotbsa.azurewebsites.net/assets/images/default-avatar.jpg" : User.AvatarUrl;
		    Name.Text = User.FullName;

		    roleLabel.Text = User.UserRole == 1 ? "Manager" : "Tranlsator";
		}

	    private async void ToProfile_Click(object sender, EventArgs e)
	    {
	        var newPage = new NavigationPage(new ProfilePage(UserService.CurrentUser));
	        await Navigation.PushAsync(newPage);
	        IsPresented = false;
        }

	    private async void ToProject_Clicked(object sender, EventArgs e)
	    {
	        Detail = new NavigationPage(new DashboardPage(new DashboardViewModel()));
	        IsPresented = false;
        }

        private async void Logout_Click(object sender, EventArgs e)
	    {
	        var newPage = new NavigationPage(new LoginPage());
            UserService.Logout();
	        await Navigation.PushModalAsync(newPage);
        }

	    private void ToTeams_Clicked(object sender, EventArgs e)
	    {
	        Detail = new NavigationPage(new TeamPage());
	        IsPresented = false;
        }
	}
}