using Polyglot.BusinessLogic.DTO;
using Polyglot.ViewModels;
using Polyglot.ViewModels.TranslationsDetails;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TranslationsPage : ContentPage
    {
        public int ComplexStringId { get; set; }

        public TranslationsPage(TranslationsViewModel translationsViewModel, int complexStringId, int projectId)
        {
            BindingContext = translationsViewModel;

            ComplexStringId = complexStringId;

            InitializeComponent();

            translationsViewModel.Initialize(complexStringId, projectId);
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ((ListView)sender).SelectedItem = null;

            var translation = e.Item as TranslationViewModel;

            var actionSheet = "";

            if (string.IsNullOrEmpty(translation.Id))
            {
                actionSheet = await DisplayActionSheet("Choose the option", "Cancel", null, "Create translation", "Comments");
            }
            else
            {
                actionSheet = await DisplayActionSheet("Choose the option", "Cancel", null, "Edit translation", "Comments", "History", "Optional translations");
            }
            

            switch (actionSheet)
            {
                case "Cancel":
                    break;

                case "Comments":
                    var commentsPage = new CommentsPage(new CommentsViewModel(), ComplexStringId);
                    await Navigation.PushAsync(commentsPage);
                    break;

                case "History":                   
                    var historyPage = new HistoryPage(new HistoryViewModel(), ComplexStringId, translation.Id);
                    await Navigation.PushAsync(historyPage);

                    break;

                case "Optional translations":                   
                    var optionalPage = new OptionalTranslationsPage(new OptionalTranslationsViewModel(), ComplexStringId, translation.Id);
                    await Navigation.PushAsync(optionalPage);

                    break;

                case "Edit translation":
                    var editTranslationPage = new EditTranslationPage(ComplexStringId, translation);
                    await Navigation.PushAsync(editTranslationPage);

                    break;

                case "Create translation":
                    var createTranslationPage = new CreateTranslationPage(ComplexStringId, translation);
                    await Navigation.PushAsync(createTranslationPage);

                    break;
            }

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
