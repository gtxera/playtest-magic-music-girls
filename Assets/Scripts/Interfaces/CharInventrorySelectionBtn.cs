using UnityEngine;
using UnityEngine.UI;

public class CharInventrorySelectionBtn : MonoBehaviour
{
    private InventoryInterfaceController interfaceController;
    public PartyCharacterData characterData;

    private void Awake()
    {
        interfaceController = FindObjectOfType<InventoryInterfaceController>();
    }

    private void Start()
    {
        if (characterData != null)
        {
            gameObject.GetComponent<Image>().sprite = characterData.Icon;
        }
    }

    public void UpdateCharacterSelected()
    {
        //interfaceController.UpdateCharactersInfos();
    }
}
