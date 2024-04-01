using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondJumpState : PlayerState
{
    public PlayerSecondJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.hasSecondJump = true;
        player.SetVelocity(rb.velocity.x, player.secondJumpPower);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (Input.GetKey(KeyCode.S))
        {
            player.SetVelocity(rb.velocity.x, rb.velocity.y - player.fallPower);
        }
    }

    public override void Update()
    {
        base.Update();

        if(player.CanClimb() && !player.IsWallDetected())
        {
            stateMachine.ChangeState(player.climbState);
        }
    }

}
