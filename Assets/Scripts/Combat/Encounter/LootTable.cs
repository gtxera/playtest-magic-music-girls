using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class LootTable
{
    [SerializeField]
    private LootChance[] _loot;

    public IEnumerable<Loot> GetLoot()
    {
        var loot = new Dictionary<Item, int>();

        foreach (var lootChance in _loot)
        {
            var rand = Random.Range(0, 1f);
            if (rand <= lootChance.Chance)
            {
                if (loot.TryGetValue(lootChance.Item, out var value))
                    loot[lootChance.Item] = value + 1;
                else
                    loot[lootChance.Item] = 1;
            }
        }

        return loot.Select(kvp => new Loot(kvp.Value, kvp.Key));
    }
}

[Serializable]
public class LootChance
{
    [field: SerializeField, Range(0f, 1f)]
    public float Chance { get; private set; }

    [field: SerializeField]
    public Item Item { get; private set; }
}

public class Loot
{
    public readonly int Count;
    public readonly Item Item;

    public Loot(int count, Item item)
    {
        Count = count;
        Item = item;
    }
}