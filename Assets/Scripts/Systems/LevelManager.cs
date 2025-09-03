using Unity.AI.Navigation;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // public static LevelManager instance;
    [SerializeField] NavMeshSurface navMesh;
    [SerializeField] GameObject level1;
    [SerializeField] GameObject level1Door;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level2Door;


    void Start()
    {
        EventsManager.instance.onSpawnLevel1.AddListener(OpenDoor1);
    }

    void OnDestroy()
    {
        EventsManager.instance.onSpawnLevel1.RemoveListener(OpenDoor1);
    }

    void OpenDoor1()
    {
        level1Door.SetActive(false);
        level1.SetActive(true);
        navMesh.BuildNavMesh();
    }

    void OpenDoor2()
    {
        level2Door.SetActive(false);
        level2.SetActive(true);
        navMesh.BuildNavMesh();
    }
}
