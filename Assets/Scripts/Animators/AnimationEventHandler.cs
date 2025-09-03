using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour
{
    public UnityEvent OnFinish;
    private void AnimationFinishedTrigger() => OnFinish.Invoke();

    public void MoveForward()
    {
        transform.position += new Vector3(5, 0, 5);
    }
}
