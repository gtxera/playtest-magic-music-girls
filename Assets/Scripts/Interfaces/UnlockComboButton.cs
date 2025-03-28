using UnityEngine;
using UnityEngine.UI;

public class UnlockComboButton : UnlockButton
{
    [SerializeField]
    private Combo _combo;

    public override bool IsUnlocked() => CombosProvider.Instance.IsUnlocked(_combo);

    protected override void ConfigureLockedButton(Button lockedButton)
    {
        (lockedButton.targetGraphic as Image).sprite = _combo.Icon;
    }

    protected override void ConfigureUnlockedImage(Image image)
    {
        image.sprite = _combo.Icon;
    }

    protected override void HideDescription()
    {
        
    }

    protected override void ShowDescription()
    {
        throw new System.NotImplementedException();
    }

    protected override bool Unlock()
    {
        throw new System.NotImplementedException();
    }
}
