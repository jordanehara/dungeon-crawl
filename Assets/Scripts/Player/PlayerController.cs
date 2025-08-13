using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] EquippableAbility ability1;
    [SerializeField] EquippableAbility ability2;

    int factionID = 1;
    bool alive = true;

    private void Start()
    {
        Camera.main.gameObject.AddComponent<CameraController>();
        Camera.main.gameObject.GetComponent<CameraController>().SetFollowTarget(gameObject);
    }

    void Update()
    {
        if (!alive) return;
        if (Input.GetMouseButtonDown(0) && ability1 != null) UseAbility1();
        if (Input.GetMouseButtonDown(1) && ability2 != null) UseAbility2();
    }

    void UseAbility1()
    {
        ability1.RunAbilityClicked(this);
    }

    void UseAbility2()
    {
        ability2.RunAbilityClicked(this);
    }

    public PlayerMovement Movement()
    {
        return GetComponent<PlayerMovement>();
    }

    public PlayerAnimator GetAnimator()
    {
        return GetComponent<PlayerAnimator>();
    }

    public PlayerCharacterSheet GetCharacterSheet()
    {
        return GetComponent<PlayerCharacterSheet>();
    }

    public int GetFactionID()
    {
        return factionID;
    }

    public void TriggerDeath()
    {
        alive = false;
        GetAnimator().TriggerDeath();
    }

    public void TriggerRevive()
    {
        alive = true;
        GetAnimator().TriggerRevive();
    }
}
