using UnityEngine;

public class SkeletonAI : BasicAI
{
    enum SkeletonState { Wandering, Pursuing, Attacking, Dead };
    SkeletonState state = SkeletonState.Wandering;

    // Wandering state
    [SerializeField] float maxWanderingDistance = 6f;
    Vector3 startPosition = Vector3.zero;

    // Pursuit state
    GameObject target;
    [SerializeField] float maxPursuitDistance = 15f;
    [SerializeField] float attackRange = 1.75f;

    // Attack state
    [SerializeField] float damage = 3f;
    [SerializeField] float attackCooldown = 2.5f;
    float attackCooldownTimer = 0;
    [SerializeField] GameObject attackPrefab;

    // Death state
    [SerializeField] float experienceValue = 10f;

    private void Start()
    {
        startPosition = transform.position;
        GetNewWanderDestination();
    }

    protected override void RunAI()
    {
        switch (state)
        {
            case SkeletonState.Wandering:
                RunWandering();
                break;
            case SkeletonState.Pursuing:
                RunPursuing();
                break;
            case SkeletonState.Attacking:
                RunAttacking();
                break;
            case SkeletonState.Dead:
                break;
        }
    }

    #region Wandering
    void TriggerWandering()
    {
        state = SkeletonState.Wandering;
        GetNewWanderDestination();
    }

    void RunWandering()
    {
        Vector3 checkPosition = new Vector3(
            agent.destination.x,
            transform.position.y,
            agent.destination.z
        );

        if (Vector3.Distance(transform.position, checkPosition) < 1)
        {
            GetNewWanderDestination();
        }
    }

    void GetNewWanderDestination()
    {
        agent.destination = new Vector3(
            Random.Range(-maxWanderingDistance, maxWanderingDistance),
            startPosition.y,
            Random.Range(-maxWanderingDistance, maxWanderingDistance));
    }
    #endregion

    #region Pursuing
    void TriggerPrursuing(GameObject targetToPursue)
    {
        state = SkeletonState.Pursuing;
        target = targetToPursue;
    }

    void RunPursuing()
    {
        agent.destination = target.transform.position;

        if (DistanceToTarget() <= attackRange)
        {
            TriggerAttacking();
        }
        else if (DistanceToTarget() > maxPursuitDistance)
        {
            TriggerWandering();
        }
    }

    private float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CombatReceiver>() != null && !other.isTrigger)
        {
            if (other.GetComponent<CombatReceiver>().GetFactionID() != factionID)
            {
                TriggerPrursuing(other.gameObject);
            }
        }
    }
    #endregion

    #region Attacking
    void TriggerAttacking()
    {
        state = SkeletonState.Attacking;
        agent.destination = transform.position;
    }

    void RunAttacking()
    {
        attackCooldownTimer += Time.deltaTime;

        if (attackCooldownTimer >= attackCooldown)
        {
            attackCooldownTimer -= attackCooldown;
            SpawnAttackPrefab();
            GetComponent<EnemyAnimator>().TriggerAttack();
        }

        if (DistanceToTarget() > attackRange)
        {
            TriggerPrursuing(target);
        }
    }

    void SpawnAttackPrefab()
    {
        Vector3 attackDirection = target.transform.position - transform.position;
        Vector3 spawnPosition = (attackDirection.normalized * attackRange) + transform.position;

        GameObject newAttack = Instantiate(attackPrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<CombatActor>().SetFactionID(factionID);
        newAttack.GetComponent<CombatActor>().InitializeDamage(damage);
    }
    #endregion

    #region Dead
    public override void TriggerDeath()
    {
        state = SkeletonState.Dead;
        base.TriggerDeath();

        // Add experience
        EventsManager.instance.onExperienceGranted.Invoke(experienceValue);
    }
    #endregion
}
