using Polyglot.ViewModels;
using Polyglot.Views;

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

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ((ListView)sender).SelectedItem = null;

            var c = e.Item as ProjectViewModel;

            var projectId = c.Id;

            var newPage = new ComplexStringsPage(new ViewModels.ComplexStringsViewModel(), projectId);
            await Navigation.PushAsync(newPage);
        }
    }
}
