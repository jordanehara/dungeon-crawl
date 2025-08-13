using UnityEngine;

public class FireballEquippableAbility : EquippableAbility
{
    public override void RunAbilityClicked(PlayerController player)
    {
        myPlayer = player;
        targetedReceiver = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift)) // if hit is an enemy
            {
                SpawnEqquipedAttack(hit.point);
                AudioManager.instance.PlaySceneSwitchSwoosh();
                player.Movement().StopMoving();
            }
            else
            {
                player.Movement().MoveToLocation(hit.point);
            }
        }
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
