using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyBaseState
{
    public EnemyHitState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Enemy.Animator.SetTrigger("Hit");
    }

    public override void Exit()
    {
        
    }
    public override void HandleInput()
    {

    }
    public override void Update()
    {
        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Hit");
        if (normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {

    }
}
