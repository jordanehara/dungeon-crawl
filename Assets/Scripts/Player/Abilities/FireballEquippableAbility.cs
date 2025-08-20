using UnityEngine;

public class FireballEquippableAbility : EquippableAbility
{
    [SerializeField] float manaCost = 5;

    protected override void SuccessfulRaycastFunctionality(PlayerController player, RaycastHit hit)
    {
        if (CanCastFireball(ref hit)) // if hit is an enemy
        {
            SpawnEqquipedAttack(hit.point);
            AudioManager.instance.PlaySceneSwitchSwoosh();
            myPlayer.Movement().StopMoving();
            myPlayer.CombatReceiver().SpendMana(manaCost);
        }
        else
        {
            myPlayer.Movement().MoveToLocation(hit.point);
        }
    }

    private bool CanCastFireball(ref RaycastHit hit)
    {
        return myPlayer.CombatReceiver().GetMana() >= manaCost && (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEqquipedAttack(Vector3 location)
    {
        myPlayer.GetAnimator().TriggerAttack();
        myPlayer.transform.LookAt(new Vector3(location.x, myPlayer.transform.position.y, location.z));

        Vector3 spawnPosition = myPlayer.transform.position + myPlayer.transform.forward;

        GameObject newAttack = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<FireballCA>().SetFactionID(myPlayer.GetFactionID());
        newAttack.GetComponent<FireballCA>().SetShootDirection(myPlayer.transform.forward);
    }
}
