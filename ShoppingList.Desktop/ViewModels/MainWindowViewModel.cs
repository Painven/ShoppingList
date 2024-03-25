using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using ShoppingList.Core;
using ShoppingList.Core.Interfaces;
using ShoppingList.Desktop.BL;
using ShoppingList.Desktop.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace ShoppingList.Desktop.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly WorkingFolderWatcher watcher;
    private readonly ShoppingListApiClient apiClient;
    private readonly IShopListFileParser parser;

    [ObservableProperty]
    private ShoppingListCollection loadedList;

    [ObservableProperty]
    private string watchingFolder;

    [ObservableProperty]
    private string title = "Список покупок";

    public MainWindowViewModel(ShoppingListApiClient apiClient, IShopListFileParser parser)
    {
        this.apiClient = apiClient;
        this.parser = parser;

        WatchingFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "покупки");

        watcher = new WorkingFolderWatcher();
        watcher.FileChanged += Watcher_FileChanged;
        watcher.Watch(WatchingFolder);
    }

    private async void Watcher_FileChanged(string filePath)
    {
        var mb = MessageBox.Show($"В файле '{Path.GetFileName(filePath)}' найдены изменения\r\nОтправить его на сервер ?", "Изменения", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (mb == MessageBoxResult.Yes)
        {
            var data = parser.ParseFile(filePath);
            await apiClient.SendFile(data);
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

    [RelayCommand]
    private async Task LoadLastList()
    {
        var data = await apiClient.GetLast();

        LoadedList = new ShoppingListCollection()
        {
            Created = data.Created,
            FileId = data.FileId,
            FileName = data.FileName,
            Modified = data.Modified,
            Items = new ObservableCollection<ShoppingListItemViewModel>(data.Items.Select(i => new ShoppingListItemViewModel()
            {
                Name = i.Name,
                IsComplete = i.IsComplete,
            }))
        };

        LoadedList.Items.ToList().ForEach(i =>
        {
            i.PropertyChanged += async (o, e) =>
            {
                if (e.PropertyName == nameof(ShoppingListItemViewModel.IsComplete))
                {
                    await apiClient.SetCompleteStatus(data.FileId, i.Name, i.IsComplete);
                }
            };
        });
    }
}
