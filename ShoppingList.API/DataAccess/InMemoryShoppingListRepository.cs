using ShoppingList.Core;

namespace ShoppingList.API.DataAccess;

public class InMemoryShoppingListRepository : IShoppingListRepository
{
    private readonly List<ShoppingListFileTransferData> list = new();
    private readonly ILogger<InMemoryShoppingListRepository> logger;

    public InMemoryShoppingListRepository(ILogger<InMemoryShoppingListRepository> logger)
    {
        this.logger = logger;
    }

    public async Task AddNewListFromFile(ShoppingListFileTransferData data)
    {
        await Task.Yield();

        var exists = list.FirstOrDefault(i => i.FileId == data.FileId);
        if (exists != null)
        {
            logger.LogInformation("Старый список с id: {fileId} удален", data.FileId);
            list.Remove(exists);
        }

        logger.LogInformation("Новый список с id: {fileId} добавлен", data.FileId);
        list.Add(data);
    }

    public async Task<ShoppingListFileTransferData?> GetLastShoppingList()
    {
        await Task.Yield();

        logger.LogInformation("Запрос последнего списка");

        return list.LastOrDefault();
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
                logger.LogInformation("Обновлен статус пункта [{name}] на [{status}]", itemName, newState ? "выполнено" : "в процессе");
                item.IsComplete = newState;
            }
        }
    }
}
