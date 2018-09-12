using Polyglot.ViewModels.TranslationsDetails;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentsPage : ContentPage
    {
        public CommentsPage(CommentsViewModel commentsViewModel, int complexStringId)
        {
            BindingContext = commentsViewModel;

            InitializeComponent();

            commentsViewModel.Initialize(complexStringId);
        }
    }
}
