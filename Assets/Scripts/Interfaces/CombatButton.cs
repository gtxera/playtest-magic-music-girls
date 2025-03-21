using UnityEngine;
using UnityEngine.UI;

public class CombatButton : MonoBehaviour
{
    [SerializeField] protected BattleUIController battleUIController;
    public string buttonDescription;
    protected Button Button;

    protected virtual string ButtonDescription => buttonDescription;

    private void Awake()
    {
        Button = GetComponent<Button>();
        if(battleUIController == null) battleUIController = FindFirstObjectByType<BattleUIController>();
    }

    public void ChangeDescription()
    {
        battleUIController.ChangeOptionDescription(buttonDescription);
    }

    public void ClearDescription()
    {
        battleUIController.ChangeOptionDescription(string.Empty);
    }
}
