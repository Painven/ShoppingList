using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ShoppingList.Desktop.Models;

public partial class ShoppingListItemViewModel : ObservableObject
{
    public string Name { get; init; } = "<Без имени>";

    [ObservableProperty]
    private bool isComplete;

    public ShoppingListItemViewModel()
    {

    }

    [RelayCommand]
    private void ToggleComplete()
    {
        IsComplete = !isComplete;
    }
}
