using Unity.AI.Navigation;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] NavMeshSurface navMesh;
    [SerializeField] GameObject level1;
    [SerializeField] GameObject level1Door;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level2Door;
    [SerializeField] GameObject NPCNewPos;
    [SerializeField] GameObject NPC;

    public int defeatedEnemies = 0;

    void Start()
    {
        if (instance == null) instance = this;
        EventsManager.instance.onSpawnLevel1.AddListener(OpenDoor1);
        EventsManager.instance.onSpawnLevel2.AddListener(OpenDoor2);
        EventsManager.instance.onBossBeat.AddListener(MoveNPC);
    }

    void OnDestroy()
    {
        EventsManager.instance.onSpawnLevel1.RemoveListener(OpenDoor1);
        EventsManager.instance.onSpawnLevel2.RemoveListener(OpenDoor2);
        EventsManager.instance.onBossBeat.RemoveListener(MoveNPC);
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

    void MoveNPC()
    {
        NPC.transform.position = NPCNewPos.transform.position;
    }
}
