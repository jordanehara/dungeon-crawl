using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestChattableNPC : ChattableNPC
{
    [SerializeField] List<QuestDialog> questDialogs;
    QuestDialog activeStep;

    protected override void StartConversation()
    {
        foreach (QuestDialog questDialog in questDialogs)
        {
            if (questDialog.newStep.QuestIsOnThisStep())
            {
                EventsManager.instance.onDialogEnded.AddListener(EndOfDialogQuestTrigger);
                activeStep = questDialog;
                DialogManager.instance.TriggerDialog(NPCName, questDialog.questDialogData);
            }
        }

        if (activeStep == null) DialogManager.instance.TriggerDialog(NPCName, dialogData);
    }

    void EndOfDialogQuestTrigger()
    {
        activeStep?.newStep.ProgressQuest();
        EventsManager.instance.onDialogEnded.RemoveListener(EndOfDialogQuestTrigger);
        activeStep = null;
    }
}

[Serializable]
public class QuestDialog
{
    public QuestStep newStep;
    public List<DialogData> questDialogData = new List<DialogData>();
}