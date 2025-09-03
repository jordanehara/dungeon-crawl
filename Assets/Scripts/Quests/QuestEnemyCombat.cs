using UnityEngine;

public class QuestEnemyCombat : EnemyCR
{
    [SerializeField] QuestStep questStep;

    protected override void Die()
    {
        base.Die();
        if (questStep.QuestIsOnThisStep()) questStep.ProgressQuest();
    }

}
