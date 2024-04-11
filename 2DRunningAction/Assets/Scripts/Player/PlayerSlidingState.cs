using UnityEngine;

public class PlayerSlidingState : PlayerGroundState
{
    private bool getKey;

    public PlayerSlidingState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.cd.size = slidingSize;
        player.cd.offset = slidingOffset;

        getKey = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.cd.size = originalSize;
        player.cd.offset = originalOffset;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.SetVelocity(player.moveSpeed, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        // if (Input.GetKeyUp(KeyCode.S))
        // {
        //     stateMachine.ChangeState(player.runState);
        // }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.sIdleState);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            getKey = true;
        }

        if (getKey == true && player.CanStanding() == false)
            stateMachine.ChangeState(player.runState);
    }
}
