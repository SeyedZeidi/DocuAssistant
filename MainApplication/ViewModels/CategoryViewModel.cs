using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MainApplication.Models;

namespace MainApplication.ViewModels
{
    public class CategoryViewModel : ObservableObject
    {
        private ObservableCollection<CategoryModel> _categories;
        private CategoryModel _selectedCategory;
        private string _newCategoryName;
        private string _newCategoryDescription;

        public ObservableCollection<CategoryModel> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }

        public string NewCategoryName
        {
            get { return _newCategoryName; }
            set { SetProperty(ref _newCategoryName, value); }
        }

        public string NewCategoryDescription
        {
            get { return _newCategoryDescription; }
            set { SetProperty(ref _newCategoryDescription, value); }
        }

        public ICommand AddCategoryCommand { get; }
        public ICommand RemoveCategoryCommand { get; }
        public ICommand EditCategoryCommand { get; }

        public CategoryViewModel()
        {
            Categories = new ObservableCollection<CategoryModel>();

            AddCategoryCommand = new RelayCommand(AddCategory);
            RemoveCategoryCommand = new RelayCommand(RemoveCategory);
            EditCategoryCommand = new RelayCommand(EditCategory);
        }

        private void AddCategory()
        {
            if (string.IsNullOrEmpty(NewCategoryName))
            {
                MessageBox.Show("Please enter a category name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Generate a unique category id 
            int newCategoryId = GenerateUniqueId();

            // Create a new category and add it to the collection
            CategoryModel newCategory = new CategoryModel(newCategoryId, NewCategoryName, NewCategoryDescription);
            Categories.Add(newCategory);

            // Clear the input fields
            NewCategoryName = string.Empty;
            NewCategoryDescription = string.Empty;
        }

        private void RemoveCategory()
        {
            if (SelectedCategory != null)
            {
                Categories.Remove(SelectedCategory);
            }
        }

        private void EditCategory()
        {
            if (SelectedCategory != null)
            {
                string updatedName = GetUpdatedCategoryNameFromUser(SelectedCategory.CategoryName);
                string updatedDescription = GetUpdatedCategoryDescriptionFromUser(SelectedCategory.Description);

                // Update the selected category
                SelectedCategory.CategoryName = updatedName;
                SelectedCategory.Description = updatedDescription;
            }
        }

        private string GetUpdatedCategoryNameFromUser(string currentName)
        {
            return MessageBox.Show($"Enter a new name for '{currentName}':", "Edit Category", MessageBoxButton.OKCancel) == MessageBoxResult.OK
                ? "UserEnteredCategoryName"
                : currentName;
        }

        private string GetUpdatedCategoryDescriptionFromUser(string currentDescription)
        {
            return MessageBox.Show($"Enter a new description for '{currentDescription}':", "Edit Category", MessageBoxButton.OKCancel) == MessageBoxResult.OK
                ? "UserEnteredCategoryDescription"
                : currentDescription;
        }

        private int GenerateUniqueId()
        {
            return Categories.Count + 1;
        }
    }
}
