using UnityEngine;
using System;

[Serializable]
public class Quest
{
    [SerializeField] string questName;
    int currentQuestStep = 0;

    public Quest(string questName, int currentQuestStep)
    {
        this.questName = questName;
        this.currentQuestStep = currentQuestStep;
    }

    public string GetQuestName()
    {
        return questName;
    }

    public int GetQuestStep()
    {
        return currentQuestStep;
    }

    public virtual void ProgressToNextStep()
    {
        currentQuestStep++;
        EventsManager.instance.onQuestStatusChanged.Invoke();
    }
}
