using Android.App;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.DTO;
using Polyglot.ViewModels;
using Polyglot.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {

        public Dashboard(DashboardViewModel dashboard)
        {
            BindingContext = dashboard;

            InitializeComponent();

            dashboard.Initialize();

            if (dashboard == null)
            {
                list.IsVisible = false;
                DisplayAlert("Alert", "You have been alerted", "OK");
            }

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var c = e.Item as ProjectViewModel;

            var projectId = c.Id;

            var newPage = new ComplexStringsPage(new ViewModels.ComplexStringsViewModel(), projectId);
            await Navigation.PushAsync(newPage);
            ((ListView)sender).SelectedItem = null;
        }
    }
}
