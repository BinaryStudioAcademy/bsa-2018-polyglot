using Polyglot.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComplexStringsPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public ComplexStringsPage(ViewModels.ComplexStringsViewModel complexStringsViewModel, int projectId)
        {
            BindingContext = complexStringsViewModel;
            InitializeComponent();

            complexStringsViewModel.Initialize(projectId);

            if (complexStringsViewModel.ComplexStrings == null||!complexStringsViewModel.ComplexStrings.Any())
            {
                DisplayAlert("Alert", "You have been alerted", "OK");
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var c = e.Item as ComplexStringViewModel;

            var stringId = c.Id;

            var newPage = new TranslationsPage(new ViewModels.TranslationsViewModel(), stringId);
            await Navigation.PushAsync(newPage);
            ((ListView)sender).SelectedItem = null;
        }
    }
}
