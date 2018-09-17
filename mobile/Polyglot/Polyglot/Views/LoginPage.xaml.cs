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
            _vm = new LoginViewModel();
		    BindingContext = this;
		    IsBusy = false;
		}

	    private async void Login_OnClicked(object sender, EventArgs e)
	    {
	        IsBusy = true;
            loginBtn.IsVisible = false;
	        UserService.Token = await _vm.LoginByEmail(Email.Text, Password.Text);

	        if (UserService.Token != string.Empty)
	        {
	            //UserService.Token = await _vm.LoginByEmail("89e75c7d12@mailox.biz", "qwerty123");
	            await UserService.GetCurrentUserInstance();
	            var newPage = new MainPage();
	            App.Current.MainPage = newPage;
	            IsBusy = false;
            }
	        else
	        {
	            IsBusy = false;
	            loginBtn.IsVisible = true;
	            await DisplayAlert("Error", "Invalid email or password", "Ok");
	        }
	    }
	}
}