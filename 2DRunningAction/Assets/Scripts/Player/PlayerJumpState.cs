using UnityEngine;


public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(1);
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
        if (rb.velocity.x <= .1f)
        {
            player.SetVelocity(player.moveSpeed, rb.velocity.y);
        }

        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (Input.GetKey(KeyCode.S))
        {
            player.SetVelocity(rb.velocity.x, rb.velocity.y - player.fallPower);
        }

        if (player.SlidButton() && player.CanStanding())
        {
            stateMachine.ChangeState(player.airState);
            //player.SetVelocity(rb.velocity.x, rb.velocity.y - player.fallPower);
        }
    }

    public override void Update()
    {
        base.Update();

        if (player.hasSecondJump == false && player.SecondJumpButton())
        {
            stateMachine.ChangeState(player.secondJumpState);
        }

        if (player.hasSecondJump == false && Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.secondJumpState);
        }

        //if(player.CanClimb())
        //{
        //    stateMachine.ChangeState(player.climbState);
        //}
    }
}