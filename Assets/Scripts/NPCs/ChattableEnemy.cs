using System.Collections.Generic;
using UnityEngine;

public class ChattableEnemy : MonoBehaviour
{
    [SerializeField] protected string NPCName = "";
    [SerializeField] protected List<DialogData> beforeFightDialog = new List<DialogData>();
    [SerializeField] protected List<DialogData> afterFightDialog = new List<DialogData>();

    bool clicked = false;
    bool buttonReleased = true;
    void Awake()
    {
        GetComponent<ChildrenAI>().enabled = false;
    }

    void Start()
    {
        DialogManager.instance.TriggerDialog(NPCName, beforeFightDialog);
        EventsManager.instance.onDialogEnded.AddListener(EnableAI);
    }

    void Update()
    {
        if (clicked && GetComponent<ChildrenAI>().enabled) RunClicked();
    }

    void EnableAI()
    {
        GetComponent<ChildrenAI>().enabled = true;
        GetComponent<ChattableEnemy>().enabled = false;
        EventsManager.instance.onDialogEnded.RemoveListener(EnableAI);
    }

    void OnMouseDown()
    {
        if (GetComponent<ChildrenAI>().enabled)
        {
            PlayerController.instance.Movement().MoveToLocation(transform.position);
            clicked = true;
            buttonReleased = false;
        }
    }

    protected virtual void StartConversation()
    {
        clicked = false;
        DialogManager.instance.TriggerDialog(NPCName, afterFightDialog);
        PlayerController.instance.Movement().StopMoving();
    }

    void RunClicked()
    {
        Debug.Log("Run clicked");
        if (Input.GetMouseButtonDown(0) && buttonReleased)
        {
            clicked = false;
        }
        else
        {
            buttonReleased = true;
        }

        if (Vector3.Distance(PlayerController.instance.transform.position, transform.position) < 1.5f)
        {
            StartConversation();
        }
    }
}
