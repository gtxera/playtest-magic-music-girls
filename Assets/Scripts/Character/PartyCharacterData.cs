using UnityEngine;

[CreateAssetMenu(fileName = "PartyCharacterData", menuName = "Scriptable Objects/PartyCharacterData")]
public class PartyCharacterData : CharacterData
{
    public Sprite interfaceBigBackground;
    public Sprite interfaceSmallBackground;
    public Sprite interfaceBigFrame;
    public Sprite interfaceSmallFrame;
    public Sprite interfaceThinFrame;
    public Sprite characterSelectionFrame;

    public string description;
    
    [field: SerializeField]
    public PartyUnit CombatPrefab { get; private set; }
}
