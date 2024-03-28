using ShoppingList.Core;

namespace ShoppingList.API.DataAccess;

public interface IShoppingListRepository
{
    Task<ShoppingListFileTransferData?> GetLastShoppingList();
    Task AddNewListFromFile(ShoppingListFileTransferData data);
    Task SetItemState(string fileId, string itemName, bool newState);
}
