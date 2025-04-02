using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Item _item;

    [SerializeField]
    private Image _icon;

    [SerializeField]
    private TextMeshProUGUI _countText;

    public void Initialize(Item item, int count)
    {
        _item = item;
        _icon.sprite = item.Sprite;
        _countText.SetText($"x{count}");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryInterfaceController.Instance.SetItemInfos(_item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryInterfaceController.Instance.ClearItemInfos();
    }
}
