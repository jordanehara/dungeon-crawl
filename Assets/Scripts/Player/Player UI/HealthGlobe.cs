using UnityEngine;

public class HealthGlobe : MonoBehaviour
{
    [SerializeField] GameObject healthFill;

    void Start()
    {
        EventsManager.instance.onHealthChanged.AddListener(UpdateHealthBar);
    }

    private void OnDestroy()
    {
        EventsManager.instance.onHealthChanged.RemoveListener(UpdateHealthBar);
    }

    void UpdateHealthBar(float newHPPercent)
    {
        newHPPercent = System.Math.Clamp(newHPPercent, 0, 1);
        healthFill.transform.localScale = new Vector3(1, newHPPercent, 1);
    }
}
