using UnityEngine;

public class SkeletonAI : BasicAI
{
    enum SkeletonState { Wandering, Pursuing, Attacking, Dead };
    SkeletonState state = SkeletonState.Wandering;

    // Wandering state
    [SerializeField] float maxWanderingDistance = 6;
    Vector3 startPosition = Vector3.zero;

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
    void TriggerPrursuing()
    {

    }

    void RunPursuing()
    {

    }
    #endregion

    #region Attacking
    void TriggerAttacking()
    {

    }

    void RunAttacking()
    {

    }
    #endregion
}
