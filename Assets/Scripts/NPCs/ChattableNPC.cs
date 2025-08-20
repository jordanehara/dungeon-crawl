using System.Collections.Generic;
using UnityEngine;

public class ChattableNPC : MonoBehaviour
{
    [SerializeField] protected string NPCName = "";
    [SerializeField] protected List<DialogData> dialogData = new List<DialogData>();

    bool clicked = false;
    bool buttonReleased = true;

    void Update()
    {
        if (clicked) RunClicked();
    }

    void OnMouseDown()
    {
        PlayerController.instance.Movement().MoveToLocation(transform.position);
        clicked = true;
        buttonReleased = false;
    }

    protected virtual void StartConversation()
    {
        clicked = false;
        DialogManager.instance.TriggerDialog(NPCName, dialogData);
        PlayerController.instance.Movement().StopMoving();
    }

    void RunClicked()
    {
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
