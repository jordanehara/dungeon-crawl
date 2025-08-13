using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject playerHUD;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void HideAll()
    {
        // List of things to hide
        HidePlayerHUD();
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
}
