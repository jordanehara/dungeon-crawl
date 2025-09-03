using UnityEditor;
using UnityEngine;

public class EquippableAbility : ClassSkill
{
    [SerializeField] protected GameObject spawnablePrefab;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] float attackCooldown = 2.5f;
    float attackCooldownTimer = 0;

    protected CombatReceiver targetedReceiver;
    protected PlayerController myPlayer;

    void Awake()
    {
        attackCooldownTimer = attackCooldown;
    }

    public virtual void RunAbilityClicked(PlayerController player)
    {
        if (MouseIsOverUI()) return;

        myPlayer = player;
        targetedReceiver = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.queriesHitTriggers = false;

        if (Physics.Raycast(ray, out hit))
        {
            SuccessfulRaycastFunctionality(player, hit);
        }
    }

    protected virtual void SuccessfulRaycastFunctionality(PlayerController player, RaycastHit hit)
    {
        player.Movement().MoveToLocation(hit.point);

        if (hit.collider.gameObject.GetComponent<Clickable>()) // if hit is an enemy
        {
            targetedReceiver = hit.collider.GetComponent<CombatReceiver>();
        }
    }

    protected virtual void SpawnEqquipedAttack(Vector3 location)
    {
        GameObject newAttack = Instantiate(spawnablePrefab, location, Quaternion.identity);
        newAttack.GetComponent<CombatActor>().SetFactionID(myPlayer.GetFactionID());

        float critMod = 1;
        int random = Random.Range(0, 100);
        float playerDex = PlayerCharacterSheet.instance.GetDexterity();
        if (random < playerDex) critMod = 2;

        float playerStrength = PlayerCharacterSheet.instance.GetStrength();
        float calculatedDamage = playerStrength / 5 * critMod;

        newAttack.GetComponent<CombatActor>().InitializeDamage(calculatedDamage);
    }

    public virtual void CancelAbility()
    {
        targetedReceiver = null;
    }

    protected virtual void Update()
    {
        if (targetedReceiver != null) RunTargetAttack();
        attackCooldownTimer += Time.deltaTime;
    }

    protected virtual void RunTargetAttack()
    {
        if (Vector3.Distance(myPlayer.transform.position, targetedReceiver.transform.position) <= attackRange)
        {
            // in range, attack
            myPlayer.Movement().MoveToLocation(myPlayer.transform.position);
            myPlayer.transform.LookAt(targetedReceiver.transform.position);

            if (attackCooldownTimer >= attackCooldown)
            {
                attackCooldownTimer -= attackCooldown;
                SpawnEqquipedAttack(myPlayer.transform.position + myPlayer.transform.forward);
                myPlayer.GetAnimator().TriggerAttack();
                targetedReceiver = null;
            }
            else
            {
                myPlayer.GetAnimator().TriggerIdle();
            }
        }
        else
        {
            // chase target
            myPlayer.Movement().MoveToLocation(targetedReceiver.transform.position);
        }
    }

    public bool MouseIsOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    public override void LevelUp()
    {
        if (PlayerCharacterSheet.instance.SkillPointSpendSuccessful())
        {
            skillLevel++;
        }
    }
}