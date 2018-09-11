using Polyglot.BusinessLogic.DTO;
using Polyglot.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage(HistoryViewModel historyViewMoodel,int complexStringId, string historyId)
        {
            BindingContext = historyViewMoodel;
           
            InitializeComponent();

            historyViewMoodel.Initialize(complexStringId, historyId);
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
