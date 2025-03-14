using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInterfaceController : MonoBehaviour
{
    [Header("General Interface Elements")]
    [SerializeField] GameObject inventoryScreen;
    [SerializeField] Button inventoryButton;
    [SerializeField] GameObject characterScreen;
    [SerializeField] Button characterButton;
    [SerializeField] GameObject abilityTreeScreen;
    [SerializeField] Button abilityTreeButton;

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
    [SerializeField] GameObject character1Screen;



    private void Start()
    {
        currentPanelIndex = 0;
        UpdateInventoryPanel(currentPanelIndex);
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


}
