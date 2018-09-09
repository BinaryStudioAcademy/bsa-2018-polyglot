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
		}

	    private async void Login_OnClicked(object sender, EventArgs e)
	    {
            UserService.Token = await _vm.LoginByEmail(Email.Text, Password.Text);
	        var newPage = new Dashboard(new ViewModels.DashboardViewModel());
	        await Navigation.PushAsync(newPage);
        }
	}
}