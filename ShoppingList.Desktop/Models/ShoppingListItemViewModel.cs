using CommunityToolkit.Mvvm.ComponentModel;

namespace ShoppingList.Desktop.Models;

public partial class ShoppingListItemViewModel : ObservableObject
{
    public string Name { get; init; } = "<Без имени>";

    [ObservableProperty]
    private bool isComplete;
}
