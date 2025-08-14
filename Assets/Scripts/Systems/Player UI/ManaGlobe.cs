using UnityEngine;

public class ManaGlobe : MonoBehaviour
{
    [SerializeField] GameObject manaFill;

    void Update()
    {
        EventsManager.instance.onManaChanged.AddListener(UpdateManaBar);
    }

    private void OnDestroy()
    {
        EventsManager.instance.onManaChanged.RemoveListener(UpdateManaBar);
    }

    void UpdateManaBar(float newManaPercent)
    {
        newManaPercent = System.Math.Clamp(newManaPercent, 0, 1);
        manaFill.transform.localScale = new Vector3(1, newManaPercent, 1);
    }
}
