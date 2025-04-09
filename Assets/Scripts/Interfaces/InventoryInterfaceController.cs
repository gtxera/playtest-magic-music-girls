using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] GameObject itemsContent;
    [SerializeField] TextMeshProUGUI tittleTxt;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] TextMeshProUGUI itemEffects;
    [SerializeField] string[] inventoryTittles;
    [SerializeField] ItemBtn itemBtnPrefab;
    private int currentPanelIndex;

    [Header("Character Screen Elements")]
    [SerializeField] CharacterSelectionButton _characterSelectionButtonPrefab;
    [SerializeField] GameObject characterInfosPanel;
    [SerializeField] Button characterInfoBtn;
    [SerializeField] GameObject charAbilityTreePanel;
    [SerializeField] Button charAbilityTreeBtn;
    [SerializeField] Transform characterSelectionContent;
    [SerializeField] GameObject equippedItemImage;
    [SerializeField] Image characterSplash;
    [SerializeField] TextMeshProUGUI charNameTxt;
    [SerializeField] TextMeshProUGUI charDescriptionTxt;
    [SerializeField] TextMeshProUGUI emotionTxt;
    [SerializeField] TextMeshProUGUI virtuosityTxt;
    [SerializeField] TextMeshProUGUI enduranceTxt;
    [SerializeField] TextMeshProUGUI lifeTxt;
    [SerializeField] TextMeshProUGUI speedTxt;
    [SerializeField] TextMeshProUGUI lvlTxt;
    [SerializeField] TextMeshProUGUI expTxt;
    //objetos de mudan�a na visual da interface
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
    [SerializeField] private ComboVisualize _comboVisualizePrefab;

    [SerializeField]
    private int _currentInventory;

    public void Show()
    {
        PauseManager.Instance.Pause();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        PauseManager.Instance.Unpause();
        gameObject.SetActive(false);
    }


    private void Start()
    {
        currentPanelIndex = 0;
        UpdateInventoryPanel(currentPanelIndex);
        ChangeInfoPanel(true);
        ChangeScreen(0);
        GenerateItemButtons();
        GenerateCharacterSelectionButtons();
        GenerateCombosContent();
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
        tittleTxt.text = inventoryTittles[index];
        _currentInventory = index;
        GenerateItemButtons();
    }

    public void SetItemInfos(Item _item)
    {
        itemName.SetText(_item.name);
        itemDescription.SetText(_item.Description);
    }

    public void ClearItemInfos()
    {
        itemName.SetText(string.Empty);
        itemDescription.SetText(string.Empty);
    }

    private void GenerateItemButtons()
    {
        itemsContent.transform.DestroyAllChildren();

        IEnumerable<InventorySlot<Item>> items = _currentInventory switch
        {
            1 => Inventory.Instance.GetInventory<ConsumableItem>().Select(slot => (InventorySlot<Item>)slot),
            2 => Inventory.Instance.GetInventory<EquipmentItem>().Select(slot => (InventorySlot<Item>)slot),
            _ => Inventory.Instance.GetInventory<Item>(),
        };

        foreach (var slot in items)
        {
            var button = Instantiate(itemBtnPrefab, itemsContent.transform);
            button.Initialize(slot.Item, slot.Count);
        }
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

    public void UpdateCharactersInfos(PartyCharacter character)
    {
        var data = character.PartyCharacterData;
        characterSplash.sprite = data.Icon;
        charNameTxt.SetText($"Nome: {data.Name}");
        charDescriptionTxt.SetText($"Descri��o: {data.description}");
        virtuosityTxt.SetText($"Virtuosidade: {character.Stats.Virtuosity}");
        emotionTxt.SetText($"Emo��o: {character.Stats.Emotion}");
        lifeTxt.SetText($"Vida: {character.Health.CurrentHealth}/{character.Stats.Health}");
        speedTxt.SetText($"Tempo: {character.Stats.Tempo}");
        lvlTxt.SetText($"N�vel: {Party.Instance.Level}");
        expTxt.SetText($"Experi�ncia: {Party.Instance.Experience}");

        inventoryPanelFrame.sprite = data.interfaceBigFrame;
        inventoryPanelTittleFrame.sprite = data.interfaceThinFrame;
        inventoryPanelBackground.sprite = data.interfaceBigBackground;
        characterPortraitFrame.sprite = data.interfaceBigFrame;
        characterPortraitBackground.sprite = data.interfaceBigBackground;
        itemsPanelBackground.sprite = data.interfaceSmallBackground;
        itemsPanelFrame.sprite = data.interfaceThinFrame;
        charSelectionFrame.sprite = data.characterSelectionFrame;
    }

    private void GenerateCharacterSelectionButtons()
    {
        characterSelectionContent.transform.DestroyAllChildren();

        var characters = Party.Instance.Characters;

        var loadedInfo = false;
        foreach (var character in characters)
        {
            var button = Instantiate(_characterSelectionButtonPrefab, characterSelectionContent.transform);
            button.Initialize(character);

            if (!loadedInfo)
            {
                UpdateCharactersInfos(character);
                loadedInfo = true;
            }
        }
    }
    #endregion

    #region CollectionSettings
    public void ChangeComboDescription(string _comboDescription)
    {
        comboDescriptionTxt.text = _comboDescription;
    }

    public void GenerateCombosContent()
    {
        var combos = CombosProvider.Instance.Combos;

        foreach (var combo in combos)
        {
            var visualizer = Instantiate(_comboVisualizePrefab, combosLocation);
            visualizer.Initialize(combo);
        }
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
