using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory : PersistentSingletonBehaviour<Inventory>, IEventListener<CombatEndedEvent>
{
    private Dictionary<Item, int> _inventory;

    public float Money { get; private set; }

    public void Add(Item item, int count)
    {
        if (_inventory.TryGetValue(item, out var currentCount))
            _inventory[item] = currentCount + count;
        else
            _inventory[item] = count;
    }

    public bool TryRemove(Item item, int amount)
    {
        if (!_inventory.TryGetValue(item, out var currentCount))
            return false;

        if (currentCount < amount)
            return false;

        var newAmount = currentCount - amount;

        if (newAmount == 0)
            _inventory.Remove(item);
        else
            _inventory[item] = currentCount - amount;
        
        
        return true; 
    }

    public bool TrySell(Item item, int amount)
    {
        if (!TryRemove(item, amount))
            return false;

        var sellValue = item.SellValue * amount;
        AddMoney(sellValue);
        return true;
    }

    public void AddMoney(float amount)
    {
        Money += amount;
    }

    public IEnumerable<InventorySlot> GetInventory<T>(SortMode sortMode = SortMode.Name, bool ascending = true) where T : Item
    {
        var category = _inventory
            .Where(kvp => kvp.Key is T);

        var filtered = sortMode switch
        {
            SortMode.Name => ascending ? category.OrderBy(kvp => kvp.Key.Name) : category.OrderByDescending(kvp => kvp.Key.Name),
            SortMode.Value => ascending ? category.OrderBy(kvp => kvp.Key.Value) : category.OrderByDescending(kvp => kvp.Key.Value),
            SortMode.Count => ascending ? category.OrderBy(kvp => kvp.Value) : category.OrderByDescending(kvp => kvp.Value),
            _ => throw new ArgumentOutOfRangeException(nameof(sortMode), sortMode, null)
        };

        return filtered.Select(kvp => new InventorySlot(kvp.Key, kvp.Value));
    }

    public void Handle(CombatEndedEvent @event)
    {
        if (!@event.PlayerVictory)
            return;

        AddMoney(@event.MoneyReward);

        foreach (var loot in @event.Loot)
        {
            Add(loot.Item, loot.Count);
        }
    }

    public enum SortMode
    {
        Name,
        Value,
        Count,
    }
}

public class InventorySlot
{
    public readonly Item Item;
    public readonly int Count;

    public InventorySlot(Item item, int count)
    {
        Item = item;
        Count = count;
    }

    public InventorySlot(Item item)
    {
        Item = item;
        Count = 1;
    }

    public override bool Equals(object obj)
    {
        if (obj is not InventorySlot slot || this is null)
        {
            return false;
        }

        return Item.Equals(slot.Item);
    }

    public override int GetHashCode()
    {
        return Item.GetHashCode();
    }
}
