using UnityEngine;

public class MultiBallEquippableAbility : FireballEquippableAbility
{
    protected override void SpawnEqquipedAttack(Vector3 location)
    {
        base.SpawnEqquipedAttack(location);
        base.SpawnEqquipedAttack(location + transform.right);
        base.SpawnEqquipedAttack(location - transform.right);
    }
}
