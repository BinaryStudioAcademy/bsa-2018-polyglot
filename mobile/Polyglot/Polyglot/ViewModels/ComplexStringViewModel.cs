using App;

namespace Polyglot.ViewModels
{
    public class ComplexStringViewModel : BaseViewModel
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _key;
        public string Key
        {
            get => _key;
            set => SetProperty(ref _key, value);
        }

        private string _originalValue;
        public string OriginalValue
        {
            get => _originalValue;
            set => SetProperty(ref _originalValue, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private int _projectId;
        public int ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }


    }
}
