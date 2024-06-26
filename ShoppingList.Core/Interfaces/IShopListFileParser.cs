﻿namespace ShoppingList.Core.Interfaces;

public interface IShopListFileParser
{
    ShoppingListFileTransferData ParseFile(string filePath);
    void SaveDataToLocalFile(string workingFolder, ShoppingListFileTransferData data);
}
