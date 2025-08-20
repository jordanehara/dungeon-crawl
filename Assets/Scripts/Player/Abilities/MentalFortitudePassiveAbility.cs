using UnityEngine;

public class MentalFortitudePassiveAbility : ClassSkill
{
    public override void LevelUp()
    {
        if (PlayerCharacterSheet.instance.SkillPointSpendSuccessful())
        {
            skillLevel++;

            float manaRegenMod = 1 + skillLevel;
            PlayerController.instance.CombatReceiver().SetManaRegenMod(manaRegenMod);
        }
    }
}
