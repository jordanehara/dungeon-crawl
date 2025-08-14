using UnityEngine;

public class ManaGlobe : MonoBehaviour
{
    [SerializeField] GameObject manaFill;

    void Update()
    {
        EventsManager.instance.onHealthChanged.AddListener(UpdateManaBar);
    }

    private void OnDestroy()
    {
        EventsManager.instance.onHealthChanged.RemoveListener(UpdateManaBar);
    }

    void UpdateManaBar(float newHPPercent)
    {
        newHPPercent = System.Math.Clamp(newHPPercent, 0, 1);
        manaFill.transform.localScale = new Vector3(1, newHPPercent, 1);
    }
}
