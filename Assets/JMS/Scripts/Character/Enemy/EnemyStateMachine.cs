using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    public CharacterHealth Target { get; private set; }

    public EnemyIdleState IdleState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }
    public EnemyHitState HitState { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public Vector3 AttackDirection { get; set; }

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>();

        IdleState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);
        HitState = new EnemyHitState(this);

        MovementSpeed = enemy.Data.GroundedData.BaseSpeed;
        RotationDamping = enemy.Data.GroundedData.BaseRotationDamping;
    }
}