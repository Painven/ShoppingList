using System.Collections.ObjectModel;

namespace ShoppingList.Desktop.Models;

public class ShoppingListCollection
{
    public string FileId { get; init; }
    public string FileName { get; init; }
    public DateTime Created { get; init; }
    public DateTime Modified { get; init; }

    public ObservableCollection<ShoppingListItemViewModel> Items { get; init; } = new();
}
