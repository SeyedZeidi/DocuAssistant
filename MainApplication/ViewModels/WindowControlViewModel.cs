using MainApplication.Commands;
using System.Windows;

namespace MainApplication.ViewModels
{
    public class WindowControlViewModel
    {
        public RelayCommand MinimizeCommand { get; }
        public RelayCommand MaximizeRestoreCommand { get; }
        public RelayCommand CloseCommand { get; }

        public WindowControlViewModel()
        {
            MinimizeCommand = new RelayCommand(MinimizeWindow);
            MaximizeRestoreCommand = new RelayCommand(MaximizeRestoreWindow);
            CloseCommand = new RelayCommand(CloseWindow);
        }

        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeRestoreWindow()
        {
            var mainWindow = Application.Current.MainWindow;
            mainWindow.WindowState = mainWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void CloseWindow()
        {
            Application.Current.MainWindow.Close();
        }
    }
}
