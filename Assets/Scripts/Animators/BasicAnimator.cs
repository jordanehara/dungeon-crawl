using UnityEngine;

public class BasicAnimator : MonoBehaviour
{
    [SerializeField] protected Animator thisAnimator;
    protected Vector3 oldPos = Vector3.zero;
    protected Vector3 deltaPos = Vector3.zero;

    public virtual void TriggerIdle()
    {
        thisAnimator.SetTrigger("Idle");
    }
    public virtual void SetWalking(bool val)
    {
        thisAnimator.SetBool("Walking", val);
    }

    public virtual void TriggerAttack()
    {
        thisAnimator.SetTrigger("Attack"); // Triggers good for a just once action
    }

    public virtual void TriggerAttack2()
    {
        thisAnimator.SetTrigger("Attack 2"); // Triggers good for a just once action
    }

    public virtual void TriggerDeath()
    {
        thisAnimator.SetTrigger("Die");
    }

    public virtual void TriggerRevive()
    {
        thisAnimator.SetTrigger("Revive");
    }
}
