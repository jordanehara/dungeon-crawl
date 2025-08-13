using UnityEngine;

public class PlayerAnimator : BasicAnimator
{
    void Update()
    {
        deltaPos = transform.position - oldPos;
        if (deltaPos.sqrMagnitude > 0.01f * Time.deltaTime)
        {
            SetWalking(true);
        }
        else
        {
            SetWalking(false);
        }
        oldPos = transform.position;
    }
}