using UnityEngine;

public class ChildrenAI : BasicAI
{
    enum ChildrenState { Wandering, Pursuing, Attacking, Dead, Idle };
    ChildrenState state;

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
        attackCooldownTimer = attackCooldown;
    }

    protected override void RunAI()
    {
        switch (state)
        {
            case ChildrenState.Wandering:
                RunWandering();
                break;
            case ChildrenState.Pursuing:
                RunPursuing();
                break;
            case ChildrenState.Attacking:
                RunAttacking();
                break;
            case ChildrenState.Dead:
                break;
        }
    }

    #region Wandering
    void TriggerWandering()
    {
        state = ChildrenState.Wandering;
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
        state = ChildrenState.Pursuing;
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
        state = ChildrenState.Attacking;
        agent.destination = transform.position;
    }

    void RunAttacking()
    {
        attackCooldownTimer += Time.deltaTime;

        if (attackCooldownTimer >= attackCooldown)
        {
            attackCooldownTimer -= attackCooldown;
            SpawnAttackPrefab();
            int attackType = Random.Range(0, 2);
            if (attackType == 0)
                GetComponent<EnemyAnimator>().TriggerAttack();
            else
                GetComponent<EnemyAnimator>().TriggerAttack2();
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
        state = ChildrenState.Dead;

        // Add experience
        EventsManager.instance.onExperienceGranted.Invoke(experienceValue);
        GetComponent<ChattableEnemy>().enabled = true;

        if (!alive) return;
        alive = false;

        if (GetComponent<EnemyAnimator>() != null)
        {
            GetComponent<EnemyAnimator>().TriggerDeath();
        }

        GetComponent<SphereCollider>().enabled = false;

        agent.enabled = false; // stop moving
        EventsManager.instance.onBossBeat.Invoke();
    }
    #endregion
}
