using UnityEngine;

public class PlayerCR : CombatReceiver
{
    protected override void Start()
    {
        base.Start();
        SetFactionID(GetComponent<PlayerController>().GetFactionID());
    }

    protected override void Die()
    {
        base.Die();
        GetComponent<PlayerController>().TriggerDeath();
    }
}
