using System.Collections.ObjectModel;
using System.ComponentModel;
using MainApplication.Models;

namespace MainApplication.ViewModels
{
    public class TagViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TagModel> _tags;

        public ObservableCollection<TagModel> Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }

        public TagViewModel()
        {
            _tags = new ObservableCollection<TagModel>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
