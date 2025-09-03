using UnityEngine;

public class QuestEnemyCombat : EnemyCR
{
    [SerializeField] QuestStep questStep;

    protected override void Die()
    {
        base.Die();
        LevelManager.instance.defeatedEnemies++;
        if (questStep.QuestIsOnThisStep() && LevelManager.instance.defeatedEnemies == 13)
        {
            questStep.ProgressQuest();
            EventsManager.instance.onSpawnLevel2.Invoke();
        }
    }

}
