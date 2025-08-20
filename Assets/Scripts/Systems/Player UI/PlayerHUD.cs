using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] GameObject statLevelUpButton;
    [SerializeField] GameObject skillLevelUpButton;

    #region Event subscription
    void Start()
    {
        EventsManager.instance.onPlayerLeveledUp.AddListener(ShowStatLevelUpButton);
        EventsManager.instance.onPlayerLeveledUp.AddListener(ShowSkillLevelUpButton);
        HideStatLevelUpButton();
        HideSkilLevelUpButton();
    }

    void OnDestroy()
    {
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(ShowStatLevelUpButton);
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(ShowSkillLevelUpButton);
    }
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) ToggleCharacterStatPanel();
        if (Input.GetKeyDown(KeyCode.T)) ToggleSkillTreePanel();
    }

    public void ToggleCharacterStatPanel()
    {
        UIManager.instance.ToggleCharacterStatsPanel();
        HideStatLevelUpButton();
    }

    public void ToggleSkillTreePanel()
    {
        UIManager.instance.ToggleSkillTreePanel();
        HideSkilLevelUpButton();
    }

    #region Stat level up button
    public void HideStatLevelUpButton()
    {
        statLevelUpButton.SetActive(false);
    }

    public void ShowStatLevelUpButton()
    {
        statLevelUpButton.SetActive(true);
    }
    #endregion

    #region Skill Level Up Button
    public void HideSkilLevelUpButton()
    {
        skillLevelUpButton.SetActive(false);
    }

    public void ShowSkillLevelUpButton()
    {
        skillLevelUpButton.SetActive(true);
    }
    #endregion
}
