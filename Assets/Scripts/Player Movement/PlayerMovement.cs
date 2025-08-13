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

    void RunClickMovement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.point != Vector3.zero)
            {
                agent.destination = hit.point;
            }
        }
    }

    public void MoveToLocation(Vector3 location)
    {
        agent.destination = location;
    }
}