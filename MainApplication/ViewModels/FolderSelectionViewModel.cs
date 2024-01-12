using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.IO;
using System.Windows;
using System.Windows.Input;
using MainApplication.Models;
using System;

namespace MainApplication.ViewModels
{
    public class FolderSelectionViewModel : ObservableObject
    {
        private FolderSelectionModel _folderSelectionModel;
        private CategoryViewModel _categoryViewModel;
        private DocumentViewModel _documentViewModel;

        private string _inputFolder;
        private string _outputFolder;

        public string InputFolder
        {
            get { return _inputFolder; }
            set { SetProperty(ref _inputFolder, value); }
        }

        public string OutputFolder
        {
            get { return _outputFolder; }
            set { SetProperty(ref _outputFolder, value); }
        }

        public ICommand SelectInputFolderCommand { get; }
        public ICommand SelectOutputFolderCommand { get; }
        public ICommand CreateOutputFoldersCommand { get; }

        public FolderSelectionViewModel(
            FolderSelectionModel folderSelectionModel,
            CategoryViewModel categoryViewModel,
            DocumentViewModel documentViewModel)
        {
            _folderSelectionModel = folderSelectionModel;
            _categoryViewModel = categoryViewModel;
            _documentViewModel = documentViewModel;

            SelectInputFolderCommand = new RelayCommand(SelectInputFolder);
            SelectOutputFolderCommand = new RelayCommand(SelectOutputFolder);
            CreateOutputFoldersCommand = new RelayCommand(CreateOutputFolders);
        }

        private void SelectInputFolder()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select an input folder",
                Filter = "Folders|*.",
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "SelectFolder"
            };

            if (dialog.ShowDialog() == true)
            {
                InputFolder = Path.GetDirectoryName(dialog.FileName);
            }
        }

        private void SelectOutputFolder()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select an output folder",
                Filter = "Folders|*.",
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "SelectFolder"
            };

            if (dialog.ShowDialog() == true)
            {
                OutputFolder = Path.GetDirectoryName(dialog.FileName);
            }
        }

        private void CreateOutputFolders()
        {
            // Check if an output folder is selected
            if (string.IsNullOrEmpty(OutputFolder))
            {
                MessageBox.Show("Please select an output folder first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Iterate through categories and create subfolders
            foreach (var category in _categoryViewModel.Categories)
            {
                string categoryFolder = Path.Combine(OutputFolder, category.CategoryName);

                try
                {
                    Directory.CreateDirectory(categoryFolder);
                    MessageBox.Show($"Folder '{category.CategoryName}' created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating folder for category '{category.CategoryName}': {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // Trigger the document loading process in DocumentViewModel
            _documentViewModel.LoadDocumentsAsync(OutputFolder);
        }
    }
}
