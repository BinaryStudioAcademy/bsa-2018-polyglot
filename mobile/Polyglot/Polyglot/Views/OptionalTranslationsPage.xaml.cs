using Polyglot.ViewModels.TranslationsDetails;
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
    public partial class OptionalTranslationsPage : ContentPage
    {
        public OptionalTranslationsPage(OptionalTranslationsViewModel optionalViewMoodel, int complexStringId, string historyId)
        {
            BindingContext = optionalViewMoodel;

            InitializeComponent();

            optionalViewMoodel.Initialize(complexStringId, historyId);
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ((ListView)sender).SelectedItem = null;
        }
        }
}
