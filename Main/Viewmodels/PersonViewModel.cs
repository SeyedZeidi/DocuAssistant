using Main.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Viewmodels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private PersonModel _person;

        public PersonModel Person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged(nameof(Person));
            }
        }

        public PersonViewModel()
        {
            // Initialize the model and other setup
            Person = new PersonModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
