using UnityEngine;

public class PlayerCR : CombatReceiver
{
    protected float currentMana = 35;
    [SerializeField] protected float maxMana = 35;

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

    #region Mana management
    public float GetMana()
    {
        return currentMana;
    }

    public void SpendMana(float amount)
    {
        currentMana -= amount;
        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
    }
    #endregion
}
