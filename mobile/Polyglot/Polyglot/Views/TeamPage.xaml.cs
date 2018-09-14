using Polyglot.BusinessLogic.DTO;
using Polyglot.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeamPage : ContentPage
	{
	    public TeamViewModel _vm;
        public TeamPage ()
		{
			InitializeComponent ();
            _vm = new TeamViewModel();
		    BindingContext = _vm;
		    _vm.LoadTeams();
		}

	    //private async void RatingList_OnItemTapped(object sender, ItemTappedEventArgs e)
	    //{
	    //    if (e.Item == null)
	    //        return;

	    //    var c = e.Item as UserProfilePrevDTO;

	    //    var profileId = c.Id;

	    //    var newPage = new ProfilePage(new ProfileViewModel(),profileId);
	    //    await Navigation.PushAsync(newPage);
	    //    ((ListView)sender).SelectedItem = null;
     //   }
	}
}