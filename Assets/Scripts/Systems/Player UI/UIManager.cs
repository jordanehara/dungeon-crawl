using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject playerHUD;
    [SerializeField] GameObject characterStatsPanel;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void HideAll()
    {
        // List of things to hide
        HidePlayerHUD();
        HideCharacterStatsPanel();
    }

    #region Player HUD
    public void ShowPlayerHUD()
    {
        playerHUD.SetActive(true);
    }

    public void HidePlayerHUD()
    {
        playerHUD.SetActive(false);
    }
    #endregion

    #region Character stat panel
    public void ShowCharacterStatsPanel()
    {
        characterStatsPanel.SetActive(true);
    }

    public void HideCharacterStatsPanel()
    {
        characterStatsPanel.SetActive(false);
    }

    public void ToggleCharacterStatsPanel()
    {
        if (characterStatsPanel.activeInHierarchy) HideCharacterStatsPanel();
        else ShowCharacterStatsPanel();
    }
    #endregion
}
