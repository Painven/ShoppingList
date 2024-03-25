using System.Security.Cryptography;

namespace ShoppingList.Core;
public static class StringHelper
{
    public static string GenerateFileId(this string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Файл {filePath ?? "<empty>"} не найден");
        }
        var fi = new FileInfo(filePath);
        string dateString = fi.CreationTime.ToString();
        using SHA256 hash = SHA256.Create();
        var hashString = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dateString));
        string hexHashString = Convert.ToHexString(hashString);
        return hexHashString;
    }
}
