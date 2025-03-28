using UnityEngine;
using UnityEngine.UI;

public class UnlockSkillButton : UnlockButton
{
    [SerializeField]
    private UnlockSkillsScreen _skillScreen;

    [SerializeField]
    private Skill _skill;

    [SerializeField]
    private PartyCharacterData _characterData;

    private PartyCharacter _character;

    protected override void Start()
    {
        _character = Party.Instance.GetFromData(_characterData);

        base.Start();
    }

    public override bool IsUnlocked() => SkillsManager.Instance.IsUnlocked(_character, _skill);

    protected override void ConfigureLockedButton(Button lockedButton)
    {
        (lockedButton.targetGraphic as Image).sprite = _skill.Icon;
    }

    protected override void ConfigureUnlockedImage(Image image)
    {
        image.sprite = _skill.Icon;
    }

    protected override void HideDescription()
    {
        InventoryInterfaceController.Instance.HideAbilityDescription();
    }

    protected override void ShowDescription()
    {
        InventoryInterfaceController.Instance.ShowAbilityDescription(_skill.GetDescription(), _skill.Name);
    }

    protected override bool Unlock()
    {
        var unlocked = SkillsManager.Instance.TryUnlock(_character, _skill);
        if (unlocked)
        {
            _skillScreen.UpdateSkillPoints();
        }
        else
        {
            _skillScreen.ShakeText();
        }

        return unlocked;
    }
}
