using ShoppingList.Core;
using ShoppingList.Core.Interfaces;
using System.IO;

namespace ShoppingList.Desktop.BL;

public class ShopListTxtFileParser : IShopListFileParser
{
    public ShoppingListFileTransferData? ParseFile(string filePath)
    {
        var fi = new FileInfo(filePath);
        var fileId = filePath.GenerateFileId();
        var lines = File.ReadAllLines(filePath)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToArray();

        if (lines.Count() == 0)
        {
            return null;
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

        return data;

    }
}