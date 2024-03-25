namespace ShoppingList.Core;

public class ShoppingListFileTransferData
{
    public string FileId { get; set; }
    public string FileName { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    public ShoppingListItem[] Items { get; set; } = Enumerable.Empty<ShoppingListItem>().ToArray();
}
