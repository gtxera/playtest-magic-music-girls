using System;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Item")]
    [field: SerializeField] 
    public string Name { get; private set; }
    
    [field: SerializeField, TextArea]
    public string Description { get; private set; }
    
    [field: SerializeField]
    public Sprite Sprite { get; private set; }
    
    [field: SerializeField]
    public float Value { get; private set; }

    public float SellValue => Value * 0.2f;

    public abstract void Use(Character character);

    public override bool Equals(object other)
    {
        if (other is not Item item || ReferenceEquals(this, null))
            return false;

        return Name == item.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Description, Sprite, Value);
    }
}
