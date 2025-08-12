using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        Camera.main.gameObject.AddComponent<CameraController>();
        Camera.main.gameObject.GetComponent<CameraController>().SetFollowTarget(gameObject);
    }

}
