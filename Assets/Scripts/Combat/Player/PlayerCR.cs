using UnityEngine;

public class PlayerCR : CombatReceiver
{
    protected float currentMana = 35;
    [SerializeField] protected float maxMana = 35;

    // Regen
    protected float healthRegenBase = 0.5f;
    protected float healthRegenMod = 1;
    protected float manaRegenBase = 0.5f;
    protected float manaRegenMod = 1;
    protected float regenUpdateTickTimer = 0;
    protected float regenUpdateTickTime = 2;

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

    protected void Update()
    {
        RunRegen();
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
        UpdatebaseRegen();

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

    #region Regen
    protected void RunRegen()
    {
        currentHP += Time.deltaTime * healthRegenBase * healthRegenMod;
        if (currentHP > maxHP) currentHP = maxHP;

        currentMana += Time.deltaTime * manaRegenBase * manaRegenMod;
        if (currentMana > maxMana) currentMana = maxMana;

        regenUpdateTickTimer += Time.deltaTime;
        if (regenUpdateTickTimer >= regenUpdateTickTime)
        {
            regenUpdateTickTimer -= regenUpdateTickTime;
            EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
            EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        }
    }

    public void SetHPRegenMod(float newMod)
    {
        healthRegenMod = newMod;
    }

    public void SetManaRegenMod(float newMod)
    {
        manaRegenMod = newMod;
    }

    protected void UpdatebaseRegen()
    {
        healthRegenBase = 0.5f + 0.01f * PlayerCharacterSheet.instance.GetVitality();
        manaRegenBase = 0.5f + 0.01f * PlayerCharacterSheet.instance.GetEnergy();
    }
    #endregion
}
