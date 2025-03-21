using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

    private SkillCooldowns _skillCooldowns;

    public abstract Sprite Icon { get; }
    public Stats Stats => Character.Stats;
    public float CurrentHealth => Health.CurrentHealth;
    public float HealthPercentage => Health.HealthPercentage;
    public bool IsAtFullHealth => Health.CurrentHealth == Health.MaxHealth;
    public bool IsDead => Health.IsDead;
    protected Health Health => Character.Health;

    public SelectionFlags SelectionFlags
    {
        get
        {
            SelectionFlags flags = 0;

            if (!IsDead)
                flags |= SelectionFlags.Alive;

            if (IsAtFullHealth)
                flags |= SelectionFlags.FullHealth;

            return flags;
        }
    }

    public event Action<HealthChangedEventArgs> HealthChanged = delegate { };
    public event Action Died = delegate { };

    public event Action Revived = delegate { };

    private void RegisterHealthCallbacks()
    {
        Health.HealthChanged += OnHealthChanged;
        Health.Died += OnDied;
        Health.Revived += OnRevived;
    }

    private void DeregisterHealthCallbacks()
    {
        Health.HealthChanged -= OnHealthChanged;
        Health.Died -= OnDied;
        Health.Revived -= OnRevived;
    }

    public virtual void DealDamage(Unit target, float initialDamage)
    {
        Debug.Log(Character.DealDamage(target.Character, initialDamage));
    }

    public virtual void Heal(float heal)
    {
        Character.Heal(heal);
    }

    public virtual void AddModifier(Modifier modifier)
    {
        Character.AddModifier(modifier);
    }

    public IEnumerable<SkillCooldown> GetSkillsCooldowns() => _skillCooldowns.GetSkills();

    protected IEnumerable<Skill> GetAvailableSkills() => _skillCooldowns.GetAvailableSkills();

    private IEnumerable<Skill> GetSkills()
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

        _skillCooldowns = new SkillCooldowns(GetSkills());
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
        
        if (!combatTargetSelector.IsSelectable(this)) 
            return;
        
        _selectionIndicator.enabled = true;
    }

    private void OnMouseExit()
    {
        _isHovering = false;
        _selectionIndicator.enabled = false;
    }

    public void Select()
    {
        if (!CombatTargetSelector.Instance.TryAddToSelection(this))
            return;

        _selectionIndicator.enabled = false;
        _selectedIndicator.enabled = true;
    }
    
    public void Deselect()
    {
        _selectedIndicator.enabled = false;
    }

    public void HideSelection()
    {
        _selectionIndicator.enabled = false;
        _selectedIndicator.enabled = false;
    }
    
    private void OnSelect(InputAction.CallbackContext _)
    {
        if (!_isHovering)
            return;
        
        Select();
    }

    private void OnDeselect(InputAction.CallbackContext _)
    {
        if (!_isHovering)
            return;
        
        if (!CombatTargetSelector.Instance.TryRemoveFromSelection(this))
            return;
        
        Deselect();
        _selectionIndicator.enabled = true;
    }

    public void Handle(CombatTurnPassedEvent @event)
    {
        if (@event.Next != this)
            return;
        
        _skillCooldowns.UpdateCooldowns();
        UniTask.RunOnThreadPool(OnUnitTurn);
    }

    public void UseSkill(Skill skill)
    {
        _skillCooldowns.GetCooldown(skill).Use();
    }

    private void OnHealthChanged(HealthChangedEventArgs args) => HealthChanged.Invoke(args);
    private void OnDied() => Died.Invoke();
    private void OnRevived() => Revived.Invoke();


    protected abstract UniTask OnUnitTurn();
}
