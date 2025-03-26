using UnityEngine;
using UnityEngine.UI;

public class ComboTreeBtn : MonoBehaviour
{
    [SerializeField] Button[] nextBtns;

    [SerializeField] string description;

    private bool picked;

    private InventoryInterfaceController inventoryController;

    private Button self;

    private void Awake()
    {
        self = GetComponent<Button>();
        inventoryController = FindObjectOfType<InventoryInterfaceController>();
    }

    public void MouseEnterBtn()
    {
        if (picked) return;
        inventoryController.UpdateAbilityDescription(description, true);
    }

    public void MouseLeaveBtn()
    {
        if (picked) return;
        inventoryController.UpdateAbilityDescription(description, false);
    }

    public void ChooseAbility()
    {
        if ( nextBtns != null)
        {   
            for (int i = 0; i < nextBtns.Length; i++)
            {
                nextBtns[i].interactable = true;
            }
        }
        self.interactable = false;
        picked = true;
        inventoryController.UpdateAbilityDescription(description, false);
    }
}
