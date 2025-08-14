using UnityEngine;

public class XPBar : MonoBehaviour
{
    [SerializeField] GameObject xpFill;

    void Start()
    {
        EventsManager.instance.onExperienceUpdated.AddListener(UpdateXPBar);
    }

    void ODestroy()
    {
        EventsManager.instance.onExperienceUpdated.RemoveListener(UpdateXPBar);
    }

    void UpdateXPBar(float newXPPercent)
    {
        newXPPercent = System.Math.Clamp(newXPPercent, 0, 1);
        xpFill.transform.localScale = new Vector3(newXPPercent, 1, 1);
    }
}
