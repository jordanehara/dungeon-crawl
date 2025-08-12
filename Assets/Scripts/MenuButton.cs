using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneChanger.instance.LoadMenuScene();
    }

    public void LoadHowToPlayScene()
    {
        SceneChanger.instance.LoadHowToPlayScene();
    }

    public void LoadCreditsScene()
    {
        SceneChanger.instance.LoadCreditsScene();
    }

    public void LoadGameScene()
    {
        SceneChanger.instance.LoadGameScene();
    }
}