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
}
