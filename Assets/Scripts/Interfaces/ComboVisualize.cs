using UnityEngine;
using UnityEngine.UI;

public class ComboVisualize : MonoBehaviour
{
    [SerializeField] Image[] _images;

    private Combo _combo;

    public void Initialize(Combo combo)
    {
        _combo = combo;
        var index = 0;
        foreach (var kvp in combo.ComboDefinition)
        {
            var sum = index + kvp.Value;
            for (var i = index; index < sum; i++)
                _images[i].sprite = EmotionIcons.Instance.GetEmotionIcon(kvp.Key);
        }
    }

    public void ChangeDescription()
    {
        InventoryInterfaceController.Instance.ChangeComboDescription(_combo.Description);
    }
}
