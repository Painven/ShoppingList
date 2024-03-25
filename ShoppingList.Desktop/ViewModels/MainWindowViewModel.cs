using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using ShoppingList.Core;
using ShoppingList.Desktop.BL;
using System.IO;
using System.Windows;

namespace ShoppingList.Desktop.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly WorkingFolderWatcher watcher;
    private readonly ShoppingListApiClient apiClient;

    [ObservableProperty]
    private string watchingFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

    [ObservableProperty]
    private string title = "Список покупок";

    public MainWindowViewModel(ShoppingListApiClient apiClient)
    {
        this.apiClient = apiClient;
        watcher = new WorkingFolderWatcher();
        watcher.FileChanged += Watcher_FileChanged;
        watcher.Watch(WatchingFolder);
    }

    private async void Watcher_FileChanged(string filePath)
    {
        var mb = MessageBox.Show($"В файле '{Path.GetFileName(filePath)}' найдены изменения\r\nОтправить его на сервер ?", "Изменения", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (mb == MessageBoxResult.Yes)
        {
            await apiClient.SendFile(filePath);
        }
    }

    [RelayCommand]
    private void SelectWorkingFolder()
    {
        var ofd = new OpenFolderDialog()
        {
            Title = "Рабочая папка для слежения за файлами"
        };
        if (ofd.ShowDialog() == true)
        {
            watcher.FileChanged -= Watcher_FileChanged;
            WatchingFolder = ofd.FolderName;
            watcher.Watch(WatchingFolder);
        };

    }
}
