using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundedData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    } 

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }


    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    public virtual void Update()
    {
        
    }

    private void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
        stateMachine.Player.Input.PlayerActions.Run.started += OnRunStarted;
        stateMachine.Player.Input.PlayerActions.Run.canceled += OnRunCanceled;

        stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;

        stateMachine.Player.Input.PlayerActions.Attack.performed += OnAttackPerformed;
        stateMachine.Player.Input.PlayerActions.Attack.canceled += OnAttackCanceled;
    }

    private void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        stateMachine.Player.Input.PlayerActions.Run.started -= OnRunStarted;
        stateMachine.Player.Input.PlayerActions.Run.canceled -= OnRunCanceled;

        stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;

        stateMachine.Player.Input.PlayerActions.Attack.performed -= OnAttackPerformed;
        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        
    }
    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        
    }
    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
        
    }
    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
        
    }
    protected virtual void OnAttackPerformed(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = true;
    }
    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = false;
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    protected Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Rotate(Vector3 movementDirection)
    {
        if(movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.Player.transform.rotation = Quaternion.Slerp(stateMachine.Player.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.fixedDeltaTime);
        }
    }

    protected virtual void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovemenetSpeed();
        stateMachine.Player.Controller.Move(
            ((movementDirection * movementSpeed)
            + stateMachine.Player.ForceReceiver.Movement)
            * Time.fixedDeltaTime
            );
    }
    protected float GetMovemenetSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }
    
    protected void ForceMove()
    {
        stateMachine.Player.Controller.Move(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }
    
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}