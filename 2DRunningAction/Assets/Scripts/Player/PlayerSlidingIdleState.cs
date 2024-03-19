using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerSlidingIdleState : PlayerGroundState
{
    public PlayerSlidingIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocity(player.moveSpeed, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.S))
        {
            stateMachine.ChangeState(player.runState);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.sIdleState);
        }
    }
}
