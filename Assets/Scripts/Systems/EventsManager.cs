using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;

    public UnityEvent<float> onExperienceGranted;
    public UnityEvent<float> onExperienceUpdated;
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<float> onManaChanged;
    public UnityEvent onPlayerDied;
    public UnityEvent onPlayerRevived;
    public UnityEvent onPlayerLeveledUp;
    public UnityEvent onStatPointSpent;
    public UnityEvent onSkillPointSpent;
    public UnityEvent<ClassSkill> onNewAbility2Equipped; // swap to new ability
    public UnityEvent onDialogStarted;
    public UnityEvent onDialogEnded;

    void Awake()
    {
        if (instance == null) instance = this;
    }
}
