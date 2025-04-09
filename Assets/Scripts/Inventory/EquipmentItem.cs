using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentItem", menuName = "Scriptable Objects/EquipmentItem")]
public class EquipmentItem : Item
{
    [SerializeField, SerializeReference]
    private Modifier[] _modifiers;
    
    public override void Use(Character character)
    {
        
    }
}
