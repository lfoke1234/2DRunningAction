using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.hasJump = false;
        player.hasSecondJump = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            if (player.hasJump == false)
                stateMachine.ChangeState(player.jumpState);
        }

        if (Input.GetKey(KeyCode.S) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.slidingState);
        }

    }
}
