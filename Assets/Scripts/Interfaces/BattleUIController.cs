using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
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

    [SerializeField]
    private Button _skipButton;

    [Header("Energy Bar Controls")]
    [SerializeField] Slider energyBar;
    [SerializeField] float energyBarSpeed;
    private float energyValue;

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

    private void Awake()
    {
        EventBus.Instance.Subscribe(this);
        energyBar.maxValue = 100;
        //energyBar.value = energyBar.maxValue;
        
        _cancelButton.onClick.AddListener(CancelSelection);
        _confirmButton.onClick.AddListener(ConfirmSelection);
        
        descriptionTxt.SetText(string.Empty);
        
        _skipButton.onClick.AddListener(CombatManager.Instance.SkipTurn);
        
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
    }

    #region EnergyBar Update Methods
    public void UpdateEnergyBar(float value)
    {
        energyValue = value;
    }

    public void IncrementEnergyBarValue(float value)
    {
        energyValue = Mathf.Min(value + energyValue, energyBar.maxValue);
    }
    #endregion

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

        var unit = (PartyUnit)@event.Next;
        
        descriptionTxt.SetText(string.Empty);
        
        ChangeOptionsPanel(CombatPanel.Options);
        GenerateSkillsButtons(unit);
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

    public async UniTask ShowSkillSelection(Skill skill)
    {
        await UniTask.SwitchToMainThread();
        _skillShowerPanel.SetActive(true);
        _skillShowerText.SetText(skill.BaseName);
        await UniTask.WaitForSeconds(1f);
        await UniTask.SwitchToMainThread();
        _skillShowerPanel.SetActive(false);
    }
}

public enum CombatPanel
{
    Options,
    Skills,
    Items,
    Selection
}
