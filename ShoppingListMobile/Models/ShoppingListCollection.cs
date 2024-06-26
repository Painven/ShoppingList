﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ShoppingListMobile.Models;

public class ShoppingListCollection
{
    public string FileId { get; init; }
    public string FileName { get; init; }
    public DateTime Created { get; init; }
    public DateTime Modified { get; init; }

    public ObservableCollection<ShoppingListItemViewModel> Items { get; init; } = new();
}

public partial class ShoppingListItemViewModel : ObservableObject
{
    public ShoppingListCollection ParentCollection { get; }

    public event Action<ShoppingListItemViewModel> IsCompletedStateChanged;

    public string Name { get; init; } = "<Без имени>";

    [ObservableProperty]
    private bool isCompleted = false;

    public ShoppingListItemViewModel(ShoppingListCollection parent)
    {
        this.PropertyChanged += (o, e) => IsCompletedStateChanged?.Invoke(this);
        ParentCollection = parent;
    }
}
