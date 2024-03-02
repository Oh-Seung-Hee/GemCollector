using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("references")]
    [field: SerializeField] public PlayerSO Data {  get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public CharacterHealth CharacterHealth { get; private set; }
    public PlayerStats PlayerStats { get; private set; }

    [field: SerializeField] public Weapon Weapon { get; private set; }

    public PlayerStateMachine stateMachine;

    private void Awake()
    {
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        CharacterHealth = GetComponent<CharacterHealth>();
        PlayerStats = GetComponent<PlayerStats>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        PlayerStatsInit();
        stateMachine.ChangeState(stateMachine.IdleState);
        CharacterHealth.OnDie += OnDie;
    }
    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    void PlayerStatsInit()
    {
        PlayerStats.AttackDamage = 50f;
        PlayerStats.AttackSpeed = 1f;
        PlayerStats.MoveSpeed = 1f;
        PlayerStats.JumpForce = 1f;
    }
    void OnDie()
    {
        Destroy(gameObject, 5f);
        Animator.SetTrigger("Die");
        enabled = false;
    }
}