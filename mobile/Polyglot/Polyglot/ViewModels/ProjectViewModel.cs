using App;

namespace Polyglot.ViewModels
{


    public class ProjectViewModel : BaseViewModel
    {
        private const string DefaultImageUrl = "../Assets/project.jpg";


        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);

        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);

        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);

        }

        private string _imageUrl = DefaultImageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (_imageUrl == value)
                {
                    return;
                }

                _imageUrl = string.IsNullOrEmpty(value) ? DefaultImageUrl : value;
                RaisePropertyChanged();
            }
        }


    }
}
