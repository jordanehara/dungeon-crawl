using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] GameObject statLevelUpButton;

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
