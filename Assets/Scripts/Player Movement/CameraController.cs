using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject followTarget;
    Vector3 offsetVector = new Vector3(10, 14, -10);

    void Update()
    {
        if (followTarget != null)
        {
            Follow();
        }
    }

    public void SetFollowTarget(GameObject target)
    {
        followTarget = target;
    }

    void Follow()
    {
        transform.position = followTarget.transform.position + offsetVector;
        transform.LookAt(followTarget.transform.position);
    }
}