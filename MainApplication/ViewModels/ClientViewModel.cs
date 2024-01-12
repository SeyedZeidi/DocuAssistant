using System.Collections.ObjectModel;
using System.ComponentModel;
using MainApplication.Models;

namespace MainApplication.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ClientModel> _clients;

        public ObservableCollection<ClientModel> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }

        public ClientViewModel()
        {
            _clients = new ObservableCollection<ClientModel>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
