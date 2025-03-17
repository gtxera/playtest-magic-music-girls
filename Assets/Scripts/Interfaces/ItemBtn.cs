using UnityEngine;
using UnityEngine.UI;

public class ItemBtn : MonoBehaviour
{
    private InventoryInterfaceController inventory;
    public Item item;

    private void Awake()
    {
        inventory = FindFirstObjectByType<InventoryInterfaceController>();
    }

    private void Start()
    {
        if(item != null) gameObject.GetComponent<Image>().sprite = item.Sprite;
    }

    public void ChangeInfos()
    {
        inventory.UpdateItemInfos(item);
    }
}
