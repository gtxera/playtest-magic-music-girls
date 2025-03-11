using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour
{
    [Header("UI Variables")]
    [SerializeField] GameObject optionSelection;
    [SerializeField] GameObject attackSelection;
    [SerializeField] GameObject itemsSelection;
    [SerializeField] GameObject backBtn;
    [SerializeField] TextMeshProUGUI descriptionTxt;
    [SerializeField] Image characterPortrait;

    [Header("Energy Bar Controls")]
    [SerializeField] Slider energyBar;
    [SerializeField] float energyBarSpeed;
    private float energyValue;

    [Header("Round Order Variables")]
    private List<GameObject> portraitsOnRoundOrder;
    [SerializeField] Transform RoundOrderContent;
    [SerializeField] GameObject portraitObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        energyBar.maxValue = 100;
        //energyBar.value = energyBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (energyBar != null)
        {
            if (energyBar.value != energyValue)
            {
                energyBar.value = Mathf.Lerp(energyBar.value, energyValue, energyBarSpeed);
            }
        }
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
        optionSelection.SetActive(index == 0);
        backBtn.SetActive(index != 0);
        attackSelection.SetActive(index == 1);
        itemsSelection.SetActive(index == 2);
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
}
