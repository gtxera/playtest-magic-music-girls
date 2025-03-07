using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    private Dictionary<Item, int> _iventory;

    public void Add(Item item, int count)
    {
        if (_iventory.TryGetValue(item, out var currentCount))
            _iventory[item] = currentCount + count;
        else
            _iventory[item] = count;
    }

    public bool TryRemove(Item item, int amount)
    {
        if (!_iventory.TryGetValue(item, out var currentCount))
            return false;

        if (currentCount < amount)
            return false;

        var newAmount = currentCount - amount;

        if (newAmount == 0)
            _iventory.Remove(item);
        else
            _iventory[item] = currentCount - amount;
        
        
        return true; 
    }

    public IEnumerable<(T Item, int Count)> GetInventory<T>(SortMode sortMode, bool ascending = true) where T : Item
    {
        var category = _iventory
            .Where(kvp => kvp.Key is T);

        var filtered = sortMode switch
        {
            SortMode.Name => ascending ? category.OrderBy(kvp => kvp.Key.Name) : category.OrderByDescending(kvp => kvp.Key.Name),
            SortMode.Value => ascending ? category.OrderBy(kvp => kvp.Key.Value) : category.OrderByDescending(kvp => kvp.Key.Value),
            SortMode.Count => ascending ? category.OrderBy(kvp => kvp.Value) : category.OrderByDescending(kvp => kvp.Value),
            _ => throw new ArgumentOutOfRangeException(nameof(sortMode), sortMode, null)
        };

        return filtered.Select(kvp => (kvp.Key as T, kvp.Value));
    }
    
    public enum SortMode
    {
        Name,
        Value,
        Count,
    }
}
