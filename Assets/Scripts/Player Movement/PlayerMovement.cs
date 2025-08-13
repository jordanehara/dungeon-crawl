using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
    }

    public void MoveToLocation(Vector3 location)
    {
        agent.destination = location;
    }

    public void StopMoving()
    {
        agent.destination = gameObject.transform.position;
    }
}