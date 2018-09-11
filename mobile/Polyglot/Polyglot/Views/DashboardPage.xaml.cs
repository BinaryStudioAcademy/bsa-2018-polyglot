using Polyglot.ViewModels;
using Polyglot.Views;

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

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var c = e.Item as ProjectViewModel;

            var projectId = c.Id;

            var newPage = new NavigationPage(new ComplexStringsPage(new ViewModels.ComplexStringsViewModel(), projectId));
            await Navigation.PushAsync(newPage);
            ((ListView)sender).SelectedItem = null;
        }
    }
}
