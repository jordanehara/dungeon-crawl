using UnityEngine;

public class BarrelCR : CombatReceiver
{
    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}