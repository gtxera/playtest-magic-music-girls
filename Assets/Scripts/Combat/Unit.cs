using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Unit : MonoBehaviour, IEventListener<CombatTurnPassedEvent>
{
    [SerializeField]
    private SpriteRenderer _selectionIndicator;
    
    [SerializeField]
    private SpriteRenderer _selectedIndicator;

    private bool _isHovering;

    private InputActions.ICombatActions _combatActionsCallbacks;
    
    protected Character Character;

    public Stats Stats => Character.Stats;
    protected Health Health => Character.Health;

    public event Action<HealthChangedEventArgs> HealthChanged = delegate { };
    public event Action Died = delegate { };

    public event Action Revived = delegate { };

    private void RegisterHealthCallbacks()
    {
        Health.HealthChanged += HealthChanged;
        Health.Died += Died;
        Health.Revived += Revived;
    }

    private void DeregisterHealthCallbacks()
    {
        Health.HealthChanged -= HealthChanged;
        Health.Died -= Died;
        Health.Revived -= Revived;
    }

    public virtual void DealDamage(Unit target, float initialDamage, IEnumerable<StatScaling> scalings)
    {
        Debug.Log(Character.DealDamage(target.Character, initialDamage, scalings));
    }

    public virtual void Heal(float heal)
    {
        Character.Heal(heal);
    }

    public virtual void AddModifier(Modifier modifier)
    {
        Character.AddModifier(modifier);
    }

    public IEnumerable<Skill> GetSkills()
    {
        return Character.GetSkills();
    }

    public void Initialize()
    {
        _selectionIndicator.enabled = false;
        _selectedIndicator.enabled = false;
        
        EventBus.Instance.Subscribe(this);
        RegisterHealthCallbacks();
        
        _combatActionsCallbacks = new CombatActionsCallbacks.Builder()
            .AddOnSelect(OnSelect, InputActionPhase.Performed)
            .AddOnDeselect(OnDeselect, InputActionPhase.Performed)
            .Build();
        
        Input.Instance.Add(_combatActionsCallbacks);
    }

    protected virtual void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(this);
        DeregisterHealthCallbacks();
        Input.Instance.Remove(_combatActionsCallbacks);
    }

    private void OnMouseEnter()
    {
        var combatTargetSelector = CombatTargetSelector.Instance;
        
        _isHovering = true;
        
        if (!combatTargetSelector.IsSelectable(this) || combatTargetSelector.IsSelected(this)) 
            return;
        
        _selectionIndicator.enabled = true;
    }

    private void OnMouseExit()
    {
        _isHovering = false;
        _selectionIndicator.enabled = false;
    }
    
    private void OnSelect(InputAction.CallbackContext _)
    {
        if (!_isHovering)
            return;
        
        if (!CombatTargetSelector.Instance.TryAddToSelection(this))
            return;

        _selectionIndicator.enabled = false;
        _selectedIndicator.enabled = true;
    }

    private void OnDeselect(InputAction.CallbackContext _)
    {
        if (!_isHovering)
            return;
        
        if (!CombatTargetSelector.Instance.TryRemoveFromSelection(this))
            return;

        _selectedIndicator.enabled = false;
        _selectionIndicator.enabled = true;
    }

    public void Handle(CombatTurnPassedEvent @event)
    {
        OnTurnPassed(@event.Unit);
    }

    protected abstract void OnTurnPassed(Unit currentUnit);
}
