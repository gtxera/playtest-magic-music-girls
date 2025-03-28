using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UnlockButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private UnlockButton _previous;

    [SerializeField]
    private GameObject _lockedRoot;

    [SerializeField] private GameObject _unlockedRoot;

    [SerializeField]
    private Button _lockedButton;

    [SerializeField]
    private Image _unlockedImage;

    public event Action Unlocked = delegate { };

    public abstract bool IsUnlocked();

    protected abstract bool Unlock();

    protected abstract void ConfigureLockedButton(Button lockedButton);

    protected abstract void ConfigureUnlockedImage(Image image);

    protected abstract void ShowDescription();

    protected abstract void HideDescription();


    public void OnPointerEnter(PointerEventData eventData) => ShowDescription();

    public void OnPointerExit(PointerEventData eventData) => HideDescription();

    protected virtual void Start()
    {
        if (_previous != null)
            _previous.Unlocked += InitializeButton;

        var unlocked = IsUnlocked();

        _lockedRoot.SetActive(!unlocked);
        _unlockedRoot.SetActive(unlocked);

        if (!unlocked)
        {

        }
        else
            ConfigureUnlockedImage(_unlockedImage);
    }

    private void InitializeButton()
    {
        _lockedButton.interactable = _previous.IsUnlocked();
        _lockedButton.onClick.AddListener(OnClick);
        ConfigureLockedButton(_lockedButton);
    }

    private void OnClick()
    {
        if (Unlock())
        {
            ConfigureUnlockedImage(_unlockedImage);
            Unlocked();
        }
    }
}
