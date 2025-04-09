using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboVisualize : MonoBehaviour
{
    [SerializeField] Image[] _images;

    [SerializeField]
    private TextMeshProUGUI _nome;

    private Combo _combo;

    public void Initialize(Combo combo)
    {
        _combo = combo;
        var index = 0;
        foreach (var kvp in combo.ComboDefinition)
        {
            var sum = index + kvp.Value;
            for (var i = index; i < sum; i++)
                _images[i].sprite = EmotionIcons.Instance.GetEmotionIcon(kvp.Key);

            index = sum;
        }
        _nome.SetText(_combo.Name);
    }

    public void ChangeDescription()
    {
        InventoryInterfaceController.Instance.ChangeComboDescription(_combo.Description);
    }
}
