using UnityEngine;

public class PlayerCR : CombatReceiver
{
    protected float currentMana = 35;
    [SerializeField] protected float maxMana = 35;

    protected override void Start()
    {
        base.Start();
        SetFactionID(GetComponent<PlayerController>().GetFactionID());
        EventsManager.instance.onPlayerLeveledUp.AddListener(LevelUp);
        EventsManager.instance.onStatPointSpent.AddListener(StatsChangedAdjustment);
    }


    protected virtual void OnDestroy()
    {
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(LevelUp);
        EventsManager.instance.onStatPointSpent.RemoveListener(StatsChangedAdjustment);
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

    #region Level Up Events
    void LevelUp()
    {
        currentHP = maxHP;
        currentMana = maxMana;

        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
    }

    void StatsChangedAdjustment()
    {
        float oldMaxHP = maxHP;
        float oldMaxMana = maxMana;

        maxHP = PlayerCharacterSheet.instance.GetMaxHP();
        maxMana = PlayerCharacterSheet.instance.GetMaxMana();

        currentHP += maxHP - oldMaxHP;
        currentMana += maxMana - oldMaxMana;

        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
    }
    #endregion
}
