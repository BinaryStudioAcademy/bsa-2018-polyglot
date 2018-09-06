using System;
using System.Collections.Generic;
using System.Net.Http;
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
	        var token = await _vm.LoginByEmail(Email.Text, Password.Text);
	    }
	}
}