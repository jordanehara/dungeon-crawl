using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject playerHUD;
    [SerializeField] GameObject characterStatsPanel;
    [SerializeField] GameObject skillTreePanel;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void HideAll()
    {
        // List of things to hide
        HidePlayerHUD();
        HideCharacterStatsPanel();
        HideSkillTree();
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

    #region Skill Tree
    public void ShowSkillTree()
    {
        skillTreePanel.SetActive(true);
    }

    public void HideSkillTree()
    {
        skillTreePanel.SetActive(false);
    }

    public void ToggleSkillTree()
    {
        if (skillTreePanel.activeInHierarchy) HideSkillTree();
        else ShowSkillTree();
    }
    #endregion
}
