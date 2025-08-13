using UnityEngine;

public class EnemyCR : CombatReceiver
{
    protected override void Die()
    {
        base.Die();
        GetComponent<BasicAI>().TriggerDeath();
    }
}
