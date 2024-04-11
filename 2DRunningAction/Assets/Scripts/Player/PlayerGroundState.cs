using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    protected Vector2 slidingSize;
    protected Vector2 slidingOffset;
    protected Vector2 originalSize;
    protected Vector2 originalOffset;

    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.hasJump = false;
        player.hasSecondJump = false;

        originalSize = player.cd.size;
        originalOffset = player.cd.offset;
        slidingSize = new Vector2(0.75f, 0.8f);
        slidingOffset = new Vector2(0, -0.8f);
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

        if(player.IsGroundDetected() == false)
        {
            stateMachine.ChangeState(player.airState);
        }

        //if (player.CanClimb() && !player.IsWallDetected())
        //{
        //    stateMachine.ChangeState(player.climbState);
        //}

        if (Input.GetKeyDown(KeyCode.G))
        {
            stateMachine.ChangeState(player.knockbackState);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            stateMachine.ChangeState(player.bKncokbackState);
        }
    }
}
