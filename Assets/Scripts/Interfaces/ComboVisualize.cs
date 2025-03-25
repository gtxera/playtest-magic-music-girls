using UnityEngine;
using UnityEngine.UI;

public class ComboVisualize : MonoBehaviour
{
    [SerializeField] string description;

    [SerializeField] Sprite emotionIcon1;
    [SerializeField] Sprite emotionIcon2;
    [SerializeField] Sprite emotionIcon3;
    [SerializeField] Sprite emotionIcon4;

    [SerializeField] Image image1;
    [SerializeField] Image image2;
    [SerializeField] Image image3;
    [SerializeField] Image image4;

    public InventoryInterfaceController inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<InventoryInterfaceController>();

        image1.sprite = emotionIcon1;
        image2.sprite = emotionIcon2;
        image3.sprite = emotionIcon3;
        image4.sprite = emotionIcon4;
    }

    public void ChangeDescription()
    {
        inventory.ChangeComboDescription(description);
    }
}
