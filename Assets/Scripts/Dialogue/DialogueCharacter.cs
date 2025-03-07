using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "DialogueCharacter", menuName = "Scriptable Objects/DialogueCharacter")]
public class DialogueCharacter : ScriptableObject
{
    [FormerlySerializedAs("_character")]
    [SerializeField]
    private CharacterData _characterData;

    [SerializeField]
    private Color _textColor = Color.white;

    public string Name => _characterData.Name;

    public Sprite Icon => _characterData.Icon;

    public Color TextColor => _textColor;
}
