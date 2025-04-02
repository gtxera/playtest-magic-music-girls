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
    private Animator _selectionIndicatorAnimator;

    [SerializeField]
    private UnitAnimations _animations;

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

    public virtual float DealDamage(Unit target, float initialDamage)
    {
        return Character.DealDamage(target.Character, initialDamage);
    }

    public virtual float Heal(Unit target, float heal)
    {
        return Character.HealOhter(target.Character, heal);
    }

    public virtual float AddModifier(Unit target, Modifier modifier, float energy, string animationKey)
    {
        target._animations.Play(animationKey);
        target.Character.AddModifier(modifier);
        return energy;
    }

    public IEnumerable<SkillCooldown> GetSkillsCooldowns() => _skillCooldowns.GetSkills();

    protected IEnumerable<Skill> GetAvailableSkills() => _skillCooldowns.GetAvailableSkills();

    protected abstract IEnumerable<Skill> GetSkills();

    public void Initialize()
    {
        _selectionIndicator.enabled = false;
        
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
        _isHovering = true;
        
        if (!CombatTargetSelector.Instance.IsSelectable(this)) 
            return;

        if (CombatTargetSelector.Instance.IsAllyOfSelection(this))
        {
            _selectionIndicatorAnimator.Play("Ally");
        }
        else
        {
            _selectionIndicatorAnimator.Play("Enemy");
        }
        
        _selectionIndicator.enabled = true;
        _selectionIndicator.color = new Color(1, 1, 1, 0.6f);
    }

    private void OnMouseExit()
    {
        _isHovering = false;
        
        if (!CombatTargetSelector.Instance.IsSelectable(this)) 
            return;
        
        _selectionIndicator.enabled = false;
    }

    public void Select()
    {
        if (!CombatTargetSelector.Instance.TryAddToSelection(this))
            return;
        
        if (CombatTargetSelector.Instance.IsAllyOfSelection(this))
        {
            _selectionIndicatorAnimator.Play("Ally");
        }
        else
        {
            _selectionIndicatorAnimator.Play("Enemy");
        }

        _selectionIndicator.enabled = true;
        _selectionIndicator.color = Color.white;
    }
    
    public void Deselect()
    {
        _selectionIndicator.enabled = false;
    }

    public void HideSelection()
    {
        _selectionIndicator.enabled = false;
    }
    
    private void OnSelect(InputAction.CallbackContext _)
    {
        if (!_isHovering)
            return;
        
        if (!CombatTargetSelector.Instance.TryAddToSelection(this))
            return;
        
        Select();
    }

    private void OnDeselect(InputAction.CallbackContext _)
    {
        if (!_isHovering)
            return;
        
        if (!CombatTargetSelector.Instance.TryRemoveFromSelection(this))
            return;
        
        _selectionIndicator.color = new Color(1, 1, 1, 0.6f);
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
        _animations.Play(skill.AnimationKey);
        
        switch (skill.SkillType)
        {
            case SkillType.Normal:
                _skillCooldowns.GetCooldown(skill).Use();
                break;
            case SkillType.Evolved:
                CombatManager.Instance.ComboManager.GenerateEmotion(skill.ComboEmotion);
                break;
            case SkillType.Combo:
                foreach (var unit in CombatManager.Instance.AlivePartyUnits)
                    unit._animations.Play(skill.AnimationKey);
                break;
            case SkillType.Item:
                break;
        }
    }

    public float GetPercentageOfMaxHealth(float value)
    {
        return value / Health.MaxHealth;
    }

    private void OnHealthChanged(HealthChangedEventArgs args) => HealthChanged.Invoke(args);
    private void OnDied() => Died.Invoke();
    private void OnRevived() => Revived.Invoke();


    protected abstract UniTask OnUnitTurn();
}
