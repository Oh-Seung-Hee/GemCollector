using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.SaveDirection = GetMovementDirection();

        stateMachine.MovementSpeedModifier = 0;
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }
    protected override void Rotate(Vector3 direction)
    {
        if (stateMachine.SaveDirection != Vector3.zero)
        {
            stateMachine.SaveDirection = new Vector3(stateMachine.SaveDirection.x, 0, stateMachine.SaveDirection.z);
            Quaternion targetRotation = Quaternion.LookRotation(stateMachine.SaveDirection);

            stateMachine.Player.transform.rotation = Quaternion.Slerp(stateMachine.Player.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }
    //public override void PhysicsUpdate()
    //{

    //}
}