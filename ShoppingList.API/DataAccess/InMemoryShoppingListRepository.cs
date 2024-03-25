using ShoppingList.Core;

namespace ShoppingList.API.DataAccess;

public class InMemoryShoppingListRepository : IShoppingListRepository
{
    private readonly List<ShoppingListFileTransferData> list = new();

    public async Task AddNewListFromFile(ShoppingListFileTransferData data)
    {
        await Task.Yield();

        list.Add(data);
    }

    public async Task<ShoppingListFileTransferData> GetLastShoppingList()
    {
        await Task.Yield();

        return list.Last();
    }

    public async Task SetItemState(string fileId, string itemName, bool newState)
    {
        await Task.Yield();

        var file = list.FirstOrDefault(i => i.FileId == fileId);
        if (file != null)
        {
            var item = file.Items.FirstOrDefault(i => i.Name == itemName);
            if (item != null)
            {
                item.IsComplete = newState;
            }
        }
    }
}
