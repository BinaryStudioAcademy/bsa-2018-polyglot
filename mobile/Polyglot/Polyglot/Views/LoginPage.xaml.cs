using System;
using System.Collections.Generic;
using System.Net.Http;
using Polyglot.BusinessLogic.Services;
using Polyglot.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
	    private LoginViewModel _vm;
		public LoginPage()
		{
			InitializeComponent();
		    NavigationPage.SetHasNavigationBar(this, false);
            _vm = new LoginViewModel();
		    BindingContext = this;
		    IsBusy = false;
		}

	    private async void Login_OnClicked(object sender, EventArgs e)
	    {
	        loginBtn.IsVisible = false;
	        IsBusy = true;
	        //UserService.Token = await _vm.LoginByEmail(Email.Text, Password.Text);
            UserService.Token = await _vm.LoginByEmail("01f2d5e591@nicemail.pro", "йцукен123");
	        await UserService.GetCurrentUserInstance();
	        var newPage = new NavigationPage(new MainPage());
	        await Navigation.PushModalAsync(newPage);
	        IsBusy = false;
	    }
	}
}