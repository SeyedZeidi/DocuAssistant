using MainApplication.ViewModels;
using System.Windows;

namespace MainApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void CategoryView_Loaded()
        {

        }
    }
}
