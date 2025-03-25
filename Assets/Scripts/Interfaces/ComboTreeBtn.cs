using UnityEngine;
using UnityEngine.UI;

public class ComboTreeBtn : MonoBehaviour
{
    [SerializeField] Button[] nextBtns;

    [SerializeField] string description;

    private InventoryInterfaceController inventoryController;

    private Button self;

    private void Awake()
    {
        self = GetComponent<Button>();
        inventoryController = FindObjectOfType<InventoryInterfaceController>();
    }

    public void MouseEnterBtn()
    {
        if (self.interactable == false) return;
        inventoryController.UpdateAbilityDescription(description, true);
    }

    public void MouseLeaveBtn()
    {
        if (self.interactable == false) return;
        inventoryController.UpdateAbilityDescription(description, false);
    }

    public void ChooseAbility()
    {
        for (int i = 0; i < nextBtns.Length; i++)
        {
            nextBtns[i].interactable = true;
        }
        self.interactable = false;

        inventoryController.UpdateAbilityDescription(description, false);
    }
}
