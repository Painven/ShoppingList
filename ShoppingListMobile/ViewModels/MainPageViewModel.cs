using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingList.Core;
using ShoppingListMobile.Models;

namespace ShoppingListMobile.ViewModels;
public partial class MainPageViewModel : ObservableObject
{
    private readonly ShoppingListApiClient apiClient;

    [ObservableProperty]
    private string statusString = "Данные еще не были загружены";

    [ObservableProperty]
    private bool canLoadLastList = true;

    [ObservableProperty]
    private ShoppingListCollection lastList;

    public MainPageViewModel(ShoppingListApiClient apiClient)
    {
        this.apiClient = apiClient;
    }

    [RelayCommand(CanExecute = nameof(CanLoadLastList))]
    private async Task LoadLastList()
    {
        CanLoadLastList = false;
        try
        {
            var data = await apiClient.GetLast();

            int countCompleted = data.Items.Count(i => i.IsComplete);
            int countTotal = data.Items.Count();

            LastList = MapFromFileData(data);

            StatusString = $"Загрузка выполнена {DateTime.Now}. Выполнено {countCompleted}/{countTotal}";
        }
        catch (Exception ex)
        {
            StatusString = $"Ошибка API. {ex.Message}";
        }
        finally
        {
            CanLoadLastList = true;
        }
    }

    private ShoppingListCollection MapFromFileData(ShoppingListFileTransferData data)
    {
        var result = new ShoppingListCollection()
        {
            Created = data.Created,
            FileId = data.FileId,
            FileName = data.FileName,
            Modified = data.Modified,
        };
        foreach (var item in data.Items)
        {
            var vmItem = new ShoppingListItemViewModel(result)
            {
                IsCompleted = item.IsComplete,
                Name = item.Name
            };
            vmItem.IsCompletedStateChanged += async (e) => await Item_IsCompletedStateChanged(e);
            result.Items.Add(vmItem);
        }

        return result;
    }

    private async Task Item_IsCompletedStateChanged(ShoppingListItemViewModel item)
    {
        try
        {
            await apiClient.SetCompleteStatus(item.ParentCollection.FileId, item.Name, item.IsCompleted);
        }
        catch (Exception ex)
        {
            StatusString = $"Ошибка API. {ex.Message}";
        }
        StatusString = $"Данные обновлены {DateTime.Now}";
    }
}
