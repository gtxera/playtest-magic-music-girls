using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour, IEventListener<SelectionChangedEvent>, IEventListener<CombatTurnPassedEvent>, IEventListener<CombatEndedEvent>
{
    [Header("UI Variables")]
    [SerializeField]
    private GameObject _combatPanel;
    [SerializeField] GameObject optionSelection;
    [SerializeField] GameObject attackSelection;
    [SerializeField] GameObject itemsSelection;
    [SerializeField]
    private GameObject _selectionPanel;
    [SerializeField] GameObject backBtn;
    [SerializeField] TextMeshProUGUI descriptionTxt;
    [SerializeField] Image characterPortrait;
    [SerializeField] Button upgradeAbilityBtn;

    [SerializeField]
    private Button _skipButton;

    [Header("Energy Bar Controls")]
    [SerializeField] Slider energyBar;

    [SerializeField]
    private float _energyAnimationDuration;

    [SerializeField]
    private EmotionComboIndicator _emotionComboIndicatorPrefab;

    [SerializeField]
    private Transform _emotionIndicatorContent;

    private List<EmotionComboIndicator> _emotionsIndicators = new();

    [Header("Round Order Variables")]
    private List<GameObject> portraitsOnRoundOrder;
    [SerializeField] Transform RoundOrderContent;

    [SerializeField]
    private TextMeshProUGUI _turnText;
    
    [SerializeField] GameObject portraitObject;

    [SerializeField]
    private GameObject _turnSeparator;
    
    [SerializeField]
    private int _maximumPortraits;

    [SerializeField]
    private GameObject _skillShowerPanel;

    [SerializeField]
    private TextMeshProUGUI _skillShowerText;

    [Header("Target Selection")]
    [SerializeField]
    private Button _confirmButton;

    [SerializeField]
    private Button _cancelButton;

    [Header("Skills")]
    [SerializeField]
    private SkillsButton _skillsButtonPrefab;

    [Header("Items")]
    [SerializeField]
    private RectTransform _itemsParent;

    [SerializeField]
    private ItemButton _itemButtonPrefab;

    private CombatPanel _currentPanel;
    private CombatPanel _previousPanel;

    [Header("Finish")]
    [SerializeField]
    private GameObject _victoryPanel;

    [SerializeField]
    private GameObject _defeatPanel;

    [SerializeField]
    private Button _victoryButton;

    [SerializeField]
    private Button _defeatButton;

    [SerializeField]
    private TextMeshProUGUI _victoryText;

    public event Action<bool> EvolvedStateChanged = delegate { };
    private bool _evolvedState;

    private void Awake()
    {
        EventBus.Instance.Subscribe(this);
        energyBar.maxValue = 1;
        energyBar.value = 1;

        var combatManager = CombatManager.Instance;
        
        _cancelButton.onClick.AddListener(CancelSelection);
        _confirmButton.onClick.AddListener(ConfirmSelection);
        
        descriptionTxt.SetText(string.Empty);
        
        _skipButton.onClick.AddListener(combatManager.SkipTurn);
        
        _victoryButton.onClick.AddListener(() =>
        {
            _victoryPanel.SetActive(false);
            CombatManager.Instance.FinishCombat();
        });
        _defeatButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
            PersistentSingletonBehaviour.ClearAll();
        });
        upgradeAbilityBtn.onClick.AddListener(() =>
        {
            _evolvedState = !_evolvedState;
            EvolvedStateChanged.Invoke(_evolvedState);
        });

        combatManager.ComboManager.EmotionsChanged += OnEmotionsChanged;
        combatManager.ComboManager.EnergyChanged += OnEnergyChanged;
    }

    #region Options Update Methods
    public void ChangeOptionDescription(string description)
    {
        descriptionTxt.text = description;
    }

    public void ChangeOptionsPanel(int index)
    {
        ChangeOptionsPanel((CombatPanel)index);
    }

    public void ChangeOptionsPanel(CombatPanel panel)
    {
        _previousPanel = _currentPanel;
        _currentPanel = panel;
        optionSelection.SetActive(panel == CombatPanel.Options);
        backBtn.SetActive(panel is not (CombatPanel.Options or CombatPanel.Selection));
        attackSelection.SetActive(panel == CombatPanel.Skills);
        itemsSelection.SetActive(panel == CombatPanel.Items);
        _selectionPanel.SetActive(panel == CombatPanel.Selection);
        ShowEnergyButton();
    }

    public void ChangeCharacterPortrait(Sprite newImage)
    {
        characterPortrait.sprite = newImage;
    }

    public void AddPortraitOnRoundOrder(Sprite characterImage)
    {
        GameObject _portrait = Instantiate(portraitObject, RoundOrderContent);
        _portrait.GetComponent<Image>().sprite = characterImage;
        portraitsOnRoundOrder.Add(_portrait);
    }

    public void RefreshRoundOrder()
    {
        for (int i = 0; portraitsOnRoundOrder.Count > i; i++)
        {
            Destroy(portraitsOnRoundOrder[i]);
            portraitsOnRoundOrder.RemoveAt(i);
        }
    }
    #endregion

    public void Handle(SelectionChangedEvent @event)
    {
        _confirmButton.interactable = @event.TargetsCount != 0;

        if (!@event.HasTargets)
        {
            descriptionTxt.SetText("Não existem alvos disponíveis para essa habilidade");
            return;
        }
        
        if (@event.FinishedSelecting)
            descriptionTxt.SetText("Seleção completa!");
        
        else
            descriptionTxt.SetText(GetSelectionText(@event.RemainingTargetsCount, @event.AllySelection));
    }

    private string GetSelectionText(int remainingTargetsCount, bool allySelection)
    {
        var builder = new StringBuilder($"Pode selecionar ainda {remainingTargetsCount}");

        if (allySelection)
        {
            builder.Append(remainingTargetsCount > 1 ? "aliados" : "aliado");
        }
        else
        {
            builder.Append(remainingTargetsCount > 1 ? "inimigos" : "inimigo");
        }
        
        return builder.ToString();
    }

    private void CancelSelection()
    {
        ChangeOptionsPanel(_previousPanel);
        CombatTargetSelector.Instance.CancelSelection();
        descriptionTxt.SetText(string.Empty);
    }

    private void ConfirmSelection()
    {
        _combatPanel.SetActive(false);
        
        UniTask.RunOnThreadPool(CombatTargetSelector.Instance.ConfirmSelection);
    }

    public void Handle(CombatTurnPassedEvent @event)
    {
        var isAllyTurn = @event.Next.GetType() == typeof(PartyUnit);
        _combatPanel.SetActive(isAllyTurn);
        
        GenerateTurnVisualization(@event.Turn);

        if (!isAllyTurn) 
            return;

        _evolvedState = false;
        var unit = (PartyUnit)@event.Next;
        
        descriptionTxt.SetText(string.Empty);
        
        ChangeOptionsPanel(CombatPanel.Options);
        GenerateSkillsButtons(unit);
        GenerateItemButtons(unit);
        ChangeCharacterPortrait(unit.Icon);
    }

    private void GenerateSkillsButtons(Unit unit)
    {
        attackSelection.transform.DestroyAllChildren();

        foreach (var skill in unit.GetSkillsCooldowns())
        {
            var skillButton = Instantiate(_skillsButtonPrefab, attackSelection.transform);
            skillButton.Initialize(skill, unit);
        }
    }

    private void GenerateItemButtons(Unit unit)
    {
        _itemsParent.DestroyAllChildren();

        foreach (var item in Inventory.Instance.GetInventory<ConsumableItem>())
        {
            var itemButton = Instantiate(_itemButtonPrefab, _itemsParent);
            itemButton.Initialize(item.Item, unit);
        }
    }

    private void GenerateTurnVisualization(int currentTurn)
    {
        RoundOrderContent.DestroyAllChildren();
        
        _turnText.SetText($"TURNO {currentTurn}");

        var portraitCount = 0;
        
        foreach (var unit in CombatManager.Instance.RemainingTurnOrder)
        {
            if (portraitCount > _maximumPortraits)
                return;

            InstantiatePortrait(unit, portraitCount, out portraitCount);
        }

        Instantiate(_turnSeparator, RoundOrderContent);

        var order = CombatManager.Instance.TurnOrder.ToArray();
        var orderSize = order.Length;
        var positionInOrder = 0;
        
        while (portraitCount < _maximumPortraits)
        {
            InstantiatePortrait(order[positionInOrder], portraitCount, out portraitCount);
            positionInOrder++;
            
            if (positionInOrder == orderSize)
            {
                positionInOrder = 0;
                Instantiate(_turnSeparator, RoundOrderContent);
            }
        }
    }

    private void InstantiatePortrait(Unit unit, int portraitCount, out int newPortraitCount)
    {
        newPortraitCount = portraitCount;
        var portrait = Instantiate(portraitObject, RoundOrderContent);
        portrait.GetComponent<Image>().sprite = unit.Icon;
        newPortraitCount++;
    }

    private void ShowEnergyButton()
    {
        upgradeAbilityBtn.gameObject.SetActive(_currentPanel == CombatPanel.Skills && CombatManager.Instance.ComboManager.IsEnergyFull);
    }

    public void Handle(CombatEndedEvent @event)
    {
        _combatPanel.SetActive(false);
        
        if (!@event.PlayerVictory)
        {
            _defeatPanel.SetActive(true);
            return;
        }
        
        _victoryPanel.SetActive(true);
        _victoryText.SetText($"Você ganhou {Formatting.GetFloatText(@event.ExperienceReward)} pontos de experiência e {Formatting.GetFloatText(@event.MoneyReward)} moedas");
    }

    public async UniTask ShowSkillSelection(ICombatCommand command)
    {
        await UniTask.SwitchToMainThread();
        _skillShowerPanel.SetActive(true);
        _skillShowerText.SetText(command.Name);
        await UniTask.WaitForSeconds(1f);
        await UniTask.SwitchToMainThread();
        _skillShowerPanel.SetActive(false);
    }

    private void OnEnergyChanged(float energy)
    {
        energyBar.DOValue(energy, _energyAnimationDuration).SetEase(Ease.OutExpo);
        CombatAnimationsController.Instance.AddAnimation(_energyAnimationDuration);
    }

    private void OnEmotionsChanged(IReadOnlyDictionary<ComboEmotion, int> emotions)
    { 
        foreach (var kvp in emotions)
        {
            var indicators = _emotionsIndicators.Where(i => i.Emotion == kvp.Key).ToArray();
            
            if (indicators.Length == kvp.Value)
                continue;
            if (indicators.Length < kvp.Value)
            {
                var difference = kvp.Value - indicators.Length;
                for (int i = 0; i < difference; i++)
                {
                    var indicator = Instantiate(_emotionComboIndicatorPrefab, _emotionIndicatorContent);
                    indicator.Initialize(kvp.Key);
                    _emotionsIndicators.Add(indicator);
                }
            }
            else if (indicators.Length > kvp.Value)
            {
                var difference = indicators.Length - kvp.Value;
                foreach (var indicator in indicators.Take(difference))
                {
                    _emotionsIndicators.Remove(indicator);
                    indicator.Remove(() => Destroy(indicator.gameObject));
                }
            }
        }
    }
}

public enum CombatPanel
{
    Options,
    Skills,
    Items,
    Selection
}
