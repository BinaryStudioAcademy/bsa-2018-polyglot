using App;
using Polyglot.BusinessLogic.DTO;
using Polyglot.BusinessLogic.Services;

namespace Polyglot.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public UserDTO User { get; set; }

        public ProfileViewModel()
        {
            User = UserService.CurrentUser;
        }
    }
}
