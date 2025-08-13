using UnityEngine;

public class PlayerCR : CombatReceiver
{
    protected override void Start()
    {
        base.Start();
        SetFactionID(GetComponent<PlayerController>().GetFactionID());
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
    }

    protected override void Die()
    {
        base.Die();
        GetComponent<PlayerController>().TriggerDeath();
    }
}
