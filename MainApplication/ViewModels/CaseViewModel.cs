using System.Collections.ObjectModel;
using System.ComponentModel;
using MainApplication.Models;

namespace MainApplication.ViewModels
{
    public class CaseViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CaseModel> _cases;

        public ObservableCollection<CaseModel> Cases
        {
            get { return _cases; }
            set
            {
                _cases = value;
                OnPropertyChanged(nameof(Cases));
            }
        }

        public CaseViewModel()
        {
            _cases = new ObservableCollection<CaseModel>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
