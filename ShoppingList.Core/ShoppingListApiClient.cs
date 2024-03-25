using System.Net.Http.Json;

namespace ShoppingList.Core;
public class ShoppingListApiClient
{
    private readonly HttpClient client;

    public ShoppingListApiClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task SendFile(ShoppingListFileTransferData data)
    {
        try
        {
            await client.PostAsJsonAsync("/add-from-file", data);
        }
        catch
        {

        }
    }

    public async Task<ShoppingListFileTransferData> GetLast()
    {
        try
        {
            var data = await client.GetFromJsonAsync<ShoppingListFileTransferData>("/shop-list");
            return data;
        }
        catch
        {
            return new ShoppingListFileTransferData();
        }
    }

    public async Task SetCompleteStatus(string fileId, string name, bool newStatus)
    {
        try
        {
            var data = new ShoppingListItem()
            {
                Name = name,
                IsComplete = newStatus,
            };
            var result = await client.PutAsJsonAsync($"/set-item-state/{fileId}", data);
        }
        catch
        {

        }
    }
}