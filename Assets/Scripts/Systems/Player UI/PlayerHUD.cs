using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] GameObject statLevelUpButton;

    #region Event subscription
    void Start()
    {
        EventsManager.instance.onPlayerLeveledUp.AddListener(ShowStatLevelUpButton);
        HideStatLevelUpButton();
    }

    void OnDestroy()
    {
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(ShowStatLevelUpButton);
    }
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) TogglecharacterStatPanel();
    }

    public void TogglecharacterStatPanel()
    {
        UIManager.instance.ToggleCharacterStatsPanel();
        HideStatLevelUpButton();
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
}
