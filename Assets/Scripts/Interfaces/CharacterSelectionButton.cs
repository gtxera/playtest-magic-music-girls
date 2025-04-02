using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CharacterSelectionButton : MonoBehaviour
{
    private PartyCharacter _character;

    private void Awake()
    {

    }

    public void Initialize(PartyCharacter character)
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(SelectCharacter);

        _character = character;
        var image = button.targetGraphic as Image;
        image.sprite = character.CharacterData.Icon;
    }

    private void SelectCharacter()
    {
        InventoryInterfaceController.Instance.UpdateCharactersInfos(_character);
    }
}
