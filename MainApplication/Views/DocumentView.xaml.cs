using MainApplication.ViewModels;
using System.Windows.Controls;

namespace MainApplication.Views
{
    public partial class DocumentView : Page
    {
        // Assuming you have a property to set the DataContext in the constructor
        public DocumentView(DocumentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
