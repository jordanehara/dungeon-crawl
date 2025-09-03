using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))] // Force any object with this component to also have a NavMeshAgent
public class BasicAI : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected bool alive = true;
    protected int factionID = 0;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        if (alive) RunAI();
    }

    protected virtual void RunAI()
    {

    }

    public virtual void SetFactionID(int newID)
    {
        factionID = newID;
        GetComponent<CombatReceiver>().SetFactionID(factionID);
    }

    public virtual void TriggerDeath()
    {
        if (!alive) return;
        alive = false;

        if (GetComponent<EnemyAnimator>() != null)
        {
            GetComponent<EnemyAnimator>().TriggerDeath();
        }

        Collider[] attachedColliders = GetComponents<Collider>();
        foreach (Collider collider in attachedColliders)
        {
            collider.enabled = false;
        }

        agent.enabled = false; // stop moving
    }
}
