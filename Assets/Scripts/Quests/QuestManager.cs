using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    List<Quest> activeQuests = new List<Quest>();

    public static QuestManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public Quest GetQuest(string questName)
    {
        foreach (Quest quest in activeQuests)
        {
            if (quest.GetQuestName() == questName) return quest;
        }
        return null;
    }

    public void ProgressQuestToNextStep(string questName)
    {
        foreach (Quest quest in activeQuests)
        {
            if (quest.GetQuestName() == questName) quest.ProgressToNextStep();
        }
    }

    public void AddQuest(string newQuestName, int newQuestStep)
    {
        if (GetQuest(newQuestName) == null)
        {
            activeQuests.Add(new Quest(newQuestName, newQuestStep));
            EventsManager.instance.onSpawnLevel1.Invoke();
        }
    }

    public void CompleteQuest(string questName)
    {
        if (GetQuest(questName) != null) activeQuests.Remove(GetQuest(questName));
    }
}
