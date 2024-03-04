using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public CharacterController Controller { get; private set; }
    public CharacterHealth CharacterHealth { get; private set; }

    [field: SerializeField] public Weapon Weapon { get; private set; }

    private int ItemIndex;

    public EnemyStateMachine stateMachine;

    void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        CharacterHealth = GetComponent<CharacterHealth>();

        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
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

    void OnDie()
    {
        Destroy(gameObject.transform.GetChild(1).gameObject);
        GetComponent<CharacterController>().enabled = false;
        Destroy(gameObject, 5f);
        Animator.SetTrigger("Die");
        enabled = false;
        DropItem();
    }

    void DropItem()
    {
        ItemIndex = Random.Range(0, GameManager.Instance.DropItems.Count);
        GameObject item = Instantiate(GameManager.Instance.DropItems[ItemIndex]);
        item.transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        if (item.CompareTag("Gem"))
        {
            GameManager.Instance.DropItems.RemoveAt(ItemIndex);
        }
    }
}