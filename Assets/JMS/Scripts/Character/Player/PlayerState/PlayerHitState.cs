using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        if (stateMachine.Player.Controller.isGrounded)
            stateMachine.SaveDirection = GetMovementDirection();
        stateMachine.Player.Animator.SetTrigger("Hit");
    }

    public override void Exit()
    {
        
    }
    public override void HandleInput()
    {
        
    }
    public override void Update()
    {
        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Hit");
        if (normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
    protected override void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovemenetSpeed();
        stateMachine.Player.Controller.Move(
            ((stateMachine.SaveDirection * movementSpeed)
            + stateMachine.Player.ForceReceiver.Movement)
            * Time.fixedDeltaTime
            );
    }
}
