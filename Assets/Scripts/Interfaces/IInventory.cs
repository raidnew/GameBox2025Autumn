using System;

public interface IInventory
{
    Action<IItem> ItemSelected { get; set; }
    Action<IItem> ItemAdded { get; set; }
    Action<IItem> ItemRemoved { get; set; }
}