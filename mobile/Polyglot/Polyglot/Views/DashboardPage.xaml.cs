using Newtonsoft.Json;
using Polyglot.BusinessLogic.DTO;
using Polyglot.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {

        public DashboardPage(DashboardViewModel dashboard)
        {
            BindingContext = dashboard;

            InitializeComponent();

            dashboard.Initialize();
        }

        //async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item == null)
        //        return;

        //    await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

        //    //Deselect Item
        //    ((ListView)sender).SelectedItem = null;
        //}
    }
}
