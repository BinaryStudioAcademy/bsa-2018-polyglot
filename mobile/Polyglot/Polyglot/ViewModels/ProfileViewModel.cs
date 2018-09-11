using App;
using Polyglot.BusinessLogic.DTO;
using Polyglot.BusinessLogic.Services;

namespace Polyglot.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {

        private UserDTO _user;

        public UserDTO User
        {
            get => _user;
            set
            {
                if (!SetProperty(ref _user, value))
                {
                    return;
                }
            }
        }

        public ProfileViewModel(UserDTO profile)
        {
            User = profile;
        }
    }
}
