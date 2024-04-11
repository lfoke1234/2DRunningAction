using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerSlidingIdleState : PlayerGroundState
{
    // private bool getKey;

    public PlayerSlidingIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.cd.size = slidingSize;
        player.cd.offset = slidingOffset;
        // getKey = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.cd.size = originalSize;
        player.cd.offset = originalOffset;

        // getKey = true;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocity(player.moveSpeed, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        // Debug.Log(getKey);
        // 
        // if (Input.GetKeyUp(KeyCode.S))
        // {
        //     getKey = true;
        // }
        // 
        // if (getKey == true && player.CanStanding() == false)
        //     stateMachine.ChangeState(player.runState);

    }
}
