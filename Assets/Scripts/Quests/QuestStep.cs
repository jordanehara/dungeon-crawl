using System;
using UnityEngine;

[Serializable]
public class QuestStep : MonoBehaviour
{
    [SerializeField] protected string questName = "";
    [SerializeField] protected int questStep = -1;
    [SerializeField] bool initialStep = false;
    [SerializeField] bool lastStep = false;

    #region QuestStep Utility
    public bool QuestExistsInQuestManager()
    {
        return QuestManager.instance.GetQuest(questName) != null;
    }

    public bool QuestIsOnThisStep()
    {
        bool qeustOnStep = QuestManager.instance.GetQuest(questName)?.GetQuestStep() == questStep;
        bool qeustStartable = QuestManager.instance.GetQuest(questName) == null && initialStep;
        return qeustOnStep || qeustStartable;
    }
    #endregion

    protected virtual void InitializeQuest()
    {
        if (!QuestExistsInQuestManager())
        {
            QuestManager.instance.AddQuest(questName, questStep + 1);
        }
    }

    public virtual void ProgressQuest()
    {
        if (initialStep) InitializeQuest();
        else if (lastStep) QuestManager.instance.CompleteQuest(questName);
        else
        {
            QuestManager.instance.ProgressQuestToNextStep(questName);
        }
    }
}
