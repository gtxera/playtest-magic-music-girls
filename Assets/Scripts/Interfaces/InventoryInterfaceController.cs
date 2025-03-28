using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInterfaceController : SingletonBehaviour<InventoryInterfaceController>
{
    [Header("General Interface Elements")]
    [SerializeField] GameObject inventoryScreen;
    [SerializeField] Button inventoryButton;
    [SerializeField] GameObject characterScreen;
    [SerializeField] Button characterButton;
    [SerializeField] GameObject abilityTreeScreen;
    [SerializeField] Button abilityTreeButton;
    [SerializeField] GameObject collectionScreen;
    [SerializeField] Button collectionButton;

    [Header("Inventory Screen Elements")]
    [SerializeField] GameObject allItemsPanel;
    [SerializeField] GameObject allItemsContent;
    [SerializeField] GameObject consumablePanel;
    [SerializeField] GameObject consumableContent;
    [SerializeField] GameObject equipablePanel;
    [SerializeField] GameObject equipableContent;
    [SerializeField] TextMeshProUGUI tittleTxt;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] TextMeshProUGUI itemEffects;
    [SerializeField] string[] inventoryTittles;
    [SerializeField] GameObject itemBtn;
    private int currentPanelIndex;

    [Header("Character Screen Elements")]
    [SerializeField] GameObject characterInfosPanel;
    [SerializeField] Button characterInfoBtn;
    [SerializeField] GameObject charAbilityTreePanel;
    [SerializeField] Button charAbilityTreeBtn;
    [SerializeField] Transform characterSelectionContent;
    [SerializeField] GameObject equippedItemImage;
    [SerializeField] Image characterSplash;
    [SerializeField] TextMeshProUGUI charNameTxt;
    [SerializeField] TextMeshProUGUI charDescriptionTxt;
    [SerializeField] TextMeshProUGUI statsTxt;
    [SerializeField] TextMeshProUGUI lifeTxt;
    [SerializeField] TextMeshProUGUI speedTxt;
    [SerializeField] TextMeshProUGUI expTxt;
    //objetos de mudança na visual da interface
    [SerializeField] Image inventoryPanelFrame;
    [SerializeField] Image inventoryPanelTittleFrame;
    [SerializeField] Image inventoryPanelBackground;
    [SerializeField] Image characterPortraitFrame;
    [SerializeField] Image characterPortraitBackground;
    [SerializeField] Image itemsPanelBackground;
    [SerializeField] Image itemsPanelFrame;
    [SerializeField] Image charSelectionFrame;

    [Header("Ability Tree Elements")]
    [SerializeField] TextMeshProUGUI abilityDescriptionTxt;
    [SerializeField] TextMeshProUGUI abilityNameText;
    [SerializeField] GameObject abilityDescContent;

    [Header("Collection Screen Elements")]
    [SerializeField] TextMeshProUGUI comboDescriptionTxt;
    [SerializeField] GameObject comboDescPrefab;
    [SerializeField] Transform combosLocation;




    private void Start()
    {
        currentPanelIndex = 0;
        UpdateInventoryPanel(currentPanelIndex);
        ChangeInfoPanel(true);
        ChangeScreen(0);
    }

    #region GeneralInventorySettings
    public void ChangeScreen(int index)
    {
        inventoryScreen.SetActive(index == 0);
        inventoryButton.interactable = index != 0;
        characterScreen.SetActive(index == 1);
        characterButton.interactable = index != 1;
        abilityTreeScreen.SetActive(index == 2);
        abilityTreeButton.interactable = index != 2;
        collectionScreen.SetActive(index == 3);
        collectionButton.interactable= index != 3;
    }
    #endregion

    #region InventoryScreenSettings
    public void ChangeInventoryPanelBtn(bool isRight)
    {
        if (isRight)
        {
            currentPanelIndex++;
            if (currentPanelIndex > 2)
            {
                currentPanelIndex = 0;
            }
        }
        else
        {
            currentPanelIndex--;
            if (currentPanelIndex < 0)
            {
                currentPanelIndex = 2;
            }
        }

        UpdateInventoryPanel(currentPanelIndex);
    }

    public void UpdateInventoryPanel(int index)
    { 
        allItemsPanel.SetActive(index == 0);
        consumablePanel.SetActive(index == 1);
        equipablePanel.SetActive(index == 2);

        tittleTxt.text = inventoryTittles[index];
    }

    public void UpdateItemInfos(Item _item)
    {
        itemName.name = _item.name;
        itemDescription.text = _item.Description;
    }

    public void UpdateInventory(Item _item)
    {
        //if(_item.GetComponent<>)
    }
    #endregion

    #region CharacterScreenSettings
    public void ChangeInfoPanel(bool isToInfoPanel)
    {
        characterInfosPanel.SetActive(isToInfoPanel);
        characterInfoBtn.interactable = !isToInfoPanel;
        charAbilityTreePanel.SetActive(!isToInfoPanel);
        charAbilityTreeBtn.interactable = isToInfoPanel;
    }

    public void UpdateCharactersInfos(PartyCharacterData _character)
    {
        characterSplash.sprite = _character.Icon;
        charNameTxt.text = "Nome: " + _character.name;
        charDescriptionTxt.text = "Descrição: " + _character.description;
        statsTxt.text = "Status: ";// + _charStats;
        lifeTxt.text = "Vida: ";// +  _charLife;
        speedTxt.text = "Velocidade: ";// + _charSpeed;
        expTxt.text = "Experiência: ";// + _charExp;

        inventoryPanelFrame.sprite = _character.interfaceSmallFrame;
        inventoryPanelTittleFrame.sprite = _character.interfaceSmallFrame;
        inventoryPanelBackground.sprite = _character.interfaceBigBackground;
        characterPortraitFrame.sprite = _character.interfaceBigFrame;
        characterPortraitBackground.sprite = _character.interfaceBigBackground;
        itemsPanelBackground.sprite = _character.interfaceSmallBackground;
        itemsPanelFrame.sprite = _character.interfaceThinFrame;
        charSelectionFrame.sprite = _character.characterSelectionFrame;
    }
    #endregion

    #region CollectionSettings
    public void ChangeComboDescription(string _comboDescription)
    {
        comboDescriptionTxt.text = _comboDescription;
    }

    public void UpdateCombosContent()
    {

    }
    #endregion

    public void ShowAbilityDescription(string skillDescription, string skillName)
    {
        abilityDescContent.SetActive(true);
        abilityDescriptionTxt.text = skillDescription;
        abilityNameText.SetText(skillName);
    }

    public void HideAbilityDescription()
    {
        abilityDescContent.SetActive(false);
    }
}
