using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public InventoryItem(string name, Sprite icon, int cost, int count = 1)
    {
        Name = name;
        Icon = icon;
        Cost = cost;
        Count = count;
    }

    public InventoryItem(InventoryItem copyFrom)
    {
        Name = copyFrom.Name;
        Icon = copyFrom.Icon;
        MaxStack = copyFrom.MaxStack;
        Cost = copyFrom.Cost;
        Count = copyFrom.Count;
    }

    public int ID => ItemDatabase.GetIDByName(Name);
    public string Name;
    public Sprite Icon;
    public int MaxStack = 1;
    public int Count;
    public int Cost;

    public int AddCount(int amount)
    {
        Count += amount;
        if (Count <= 0)
            GameManager.MainInventory.RemoveItem(this);
        if (Count > MaxStack)
        {
            int remainder = Count - MaxStack;
            Count = MaxStack;
            return remainder;
        }
        return 0;
    }
}
