using UnityEditor;
using UnityEngine;

public class EquippableAbility : ClassSkill
{
    [SerializeField] protected GameObject spawnablePrefab;
    [SerializeField] protected float attackRange = 1.5f;

    protected CombatReceiver targetedReceiver;
    protected PlayerController myPlayer;

    public virtual void RunAbilityClicked(PlayerController player)
    {
        myPlayer = player;
        targetedReceiver = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            player.Movement().MoveToLocation(hit.point);

            if (hit.collider.gameObject.GetComponent<Clickable>()) // if hit is an enemy
            {
                targetedReceiver = hit.collider.GetComponent<CombatReceiver>();
            }
        }
    }

    protected virtual void SpawnEqquipedAttack(Vector3 location)
    {
        GameObject newAttack = Instantiate(spawnablePrefab, location, Quaternion.identity);
        newAttack.GetComponent<CombatActor>().SetFactionID(myPlayer.GetFactionID());
    }

    public virtual void CancelAbility()
    {
        targetedReceiver = null;
    }

    protected virtual void Update()
    {
        if (targetedReceiver != null) RunTargetAttack();
    }

    protected virtual void RunTargetAttack()
    {
        if (Vector3.Distance(myPlayer.transform.position, targetedReceiver.transform.position) <= attackRange)
        {
            // in range, attack
            myPlayer.Movement().MoveToLocation(myPlayer.transform.position);

            myPlayer.transform.LookAt(targetedReceiver.transform.position);

            SpawnEqquipedAttack(myPlayer.transform.position + myPlayer.transform.forward);
            myPlayer.GetAnimator().TriggerAttack();
            targetedReceiver = null;
        }
        else
        {
            // chase target
            myPlayer.Movement().MoveToLocation(targetedReceiver.transform.position);
        }
    }
}