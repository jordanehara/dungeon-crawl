using UnityEngine;

public class CombatActor : MonoBehaviour
{
    protected int factionID = 0;
    protected float damage = 1;

    public virtual void InitializeDamage(float amount)
    {
        damage = amount;
    }

    public void SetFactionID(int newID)
    {
        factionID = newID;
    }

    public virtual void HitReceiver(CombatReceiver target)
    {
        target.TakeDamage(damage);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CombatReceiver>() != null && !other.isTrigger)
        {
            if (other.GetComponent<CombatReceiver>().GetFactionID() != factionID)
            {
                HitReceiver(other.GetComponent<CombatReceiver>());
            }
        }
    }
}