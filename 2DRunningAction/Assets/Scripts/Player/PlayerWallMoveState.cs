using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallMoveState : PlayerState
{
    public PlayerWallMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rb.isKinematic = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.isKinematic = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.SetVelocity(0, player.moveSpeed / 1.5f);

         
    }

    public override void Update()
    {
        base.Update();
       //if (player.CanClimb() == false)
       //{
       //    stateMachine.ChangeState(player.climbState);
       //}
    }
}
