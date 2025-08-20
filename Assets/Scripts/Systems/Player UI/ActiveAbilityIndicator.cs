using UnityEngine;
using UnityEngine.UI;

public class ActiveAbilityIndicator : MonoBehaviour
{
    void Start()
    {
        EventsManager.instance.onNewAbility2Equipped.AddListener(UpdateImage);
    }

    void OnDestroy()
    {
        EventsManager.instance.onNewAbility2Equipped.RemoveListener(UpdateImage);
    }

    void UpdateImage(ClassSkill newAbility)
    {
        GetComponent<Image>().sprite = newAbility.GetIconSprite();
    }
}
