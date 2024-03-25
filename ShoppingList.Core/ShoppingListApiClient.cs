using System.Net.Http.Json;

namespace ShoppingList.Core;
public class ShoppingListApiClient
{
    private readonly HttpClient client;

    public ShoppingListApiClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task SendFile(string filePath)
    {
        var fi = new FileInfo(filePath);
        var fileId = filePath.GenerateFileId();
        var lines = File.ReadAllLines(filePath)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToArray();

        if (lines.Length == 0)
        {
            return;
        }

        var data = new ShoppingListFileTransferData()
        {
            Created = fi.CreationTimeUtc,
            Modified = fi.LastWriteTimeUtc,
            FileId = fileId,
            FileName = Path.GetFileName(filePath),
            Items = lines.Select(line => new ShoppingListItem()
            {
                Name = line.Substring(4).Trim(),
                IsComplete = line[1] == '+' ? true : false,
            }).ToArray()
        };


        await client.PostAsJsonAsync("/add-from-file", data);
    }
}
