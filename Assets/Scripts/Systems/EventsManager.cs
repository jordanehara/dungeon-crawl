using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;

    public UnityEvent<float> onExperienceGranted;
    public UnityEvent<float> onexperienceUpdated;
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<float> onManaChanged;
    public UnityEvent onPlayerDied;
    public UnityEvent onPlayerRevived;
    public UnityEvent onPlayerLeveledUp;

    void Awake()
    {
        if (instance == null) instance = this;
    }
}
