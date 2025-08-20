using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI strengthText;
    [SerializeField] TextMeshProUGUI dexterityText;
    [SerializeField] TextMeshProUGUI vitalityText;
    [SerializeField] TextMeshProUGUI energyText;

    [SerializeField] List<GameObject> spendButtons = new List<GameObject>();
    #region Event Subscription
    void Start()
    {
        EventsManager.instance.onStatPointSpent.AddListener(UpdateCharacterSheetPanel);
        EventsManager.instance.onPlayerLeveledUp.AddListener(UpdateCharacterSheetPanel);
        UpdateCharacterSheetPanel();
        UIManager.instance.HideCharacterStatsPanel();
        UIManager.instance.HideSkillTreePanel();
    }

    void OnDestroy()
    {
        EventsManager.instance.onStatPointSpent.RemoveListener(UpdateCharacterSheetPanel);
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(UpdateCharacterSheetPanel);
    }
    #endregion

    void OnEnable()
    {
        if (PlayerCharacterSheet.instance != null) UpdateCharacterSheetPanel();
    }

    void UpdateCharacterSheetPanel()
    {
        if (PlayerCharacterSheet.instance.GetStatPointsToSpend() == 0) HideSpendButtons();
        else ShowSpendButtons();

        UpdateStatdisplayText();
    }

    void UpdateStatdisplayText()
    {
        int displayInt = (int)PlayerCharacterSheet.instance.GetStrength();
        strengthText.text = displayInt.ToString();

        displayInt = (int)PlayerCharacterSheet.instance.GetDexterity();
        dexterityText.text = displayInt.ToString();

        displayInt = (int)PlayerCharacterSheet.instance.GetVitality();
        vitalityText.text = displayInt.ToString();

        displayInt = (int)PlayerCharacterSheet.instance.GetEnergy();
        energyText.text = displayInt.ToString();
    }

    #region Spend button display
    void HideSpendButtons()
    {
        foreach (GameObject g in spendButtons)
        {
            g.SetActive(false);
        }
    }

    void ShowSpendButtons()
    {
        foreach (GameObject g in spendButtons)
        {
            g.SetActive(true);
        }
    }
    #endregion

    #region Spend fuctions
    public void BuyStrength()
    {
        PlayerCharacterSheet.instance.BuyStrengthPoint();
    }

    public void BuyDexterity()
    {
        PlayerCharacterSheet.instance.BuyDexterityPoint();
    }

    public void BuyVitality()
    {
        PlayerCharacterSheet.instance.BuyVitalityPoint();
    }

    public void BuyEnergy()
    {
        PlayerCharacterSheet.instance.BuyEnergyPoint();
    }
    #endregion

    public void HideCharacterStatPanel()
    {
        UIManager.instance.HideCharacterStatsPanel();
    }
}
