using UnityEngine;


public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.velocity.x, player.jumpPower);
        player.hasJump = true;
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
        

        if (player.hasSecondJump == false && Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.secondJumpState);
        }

        if(player.CanClimb())
        {
            stateMachine.ChangeState(player.climbState);
        }
    }
}