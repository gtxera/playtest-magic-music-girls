using UnityEngine;
using UnityEngine.UI;

public class CombatButton : MonoBehaviour
{
    [SerializeField] protected BattleUIController battleUIController;
    public string buttonDescription;
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        if(battleUIController == null) battleUIController = FindFirstObjectByType<BattleUIController>();
    }

    public void ChangeDescription()
    {
        battleUIController.ChangeOptionDescription(buttonDescription);
    }
}
