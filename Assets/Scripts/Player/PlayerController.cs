using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] ClassSkillManager skillManager;
    [SerializeField] EquippableAbility ability1;
    [SerializeField] EquippableAbility ability2;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

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

    #region Abilities
    void UseAbility1()
    {
        ability1.RunAbilityClicked(this);
    }

    void UseAbility2()
    {
        ability2.RunAbilityClicked(this);
    }
    public void SetAbility2(EquippableAbility newAbility)
    {
        ability2 = newAbility;
        EventsManager.instance.onNewAbility2Equipped.Invoke(ability2);
    }
    public EquippableAbility GetAbility2()
    {
        return ability2;
    }
    #endregion

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

    public PlayerCR CombatReceiver()
    {
        return GetComponent<PlayerCR>();
    }

    public ClassSkillManager SkillManager()
    {
        return skillManager;
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
