using Microsoft.Toolkit.Mvvm.ComponentModel;
using MainApplication.Models;

namespace MainApplication.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public CategoryViewModel CategoryViewModel { get; } = new CategoryViewModel();
        public FolderSelectionModel FolderSelectionModel { get; } = new FolderSelectionModel();
        public FolderSelectionViewModel FolderSelectionViewModel { get; }
        public WindowControlViewModel WindowControlViewModel { get; } = new WindowControlViewModel();

        public DocumentViewModel DocumentModel { get; }

        public MainViewModel()
        {
            DocumentModel = new DocumentViewModel(FolderSelectionViewModel, CategoryViewModel); 
            FolderSelectionViewModel = new FolderSelectionViewModel(FolderSelectionModel, CategoryViewModel, DocumentModel);
        }
    }

}
