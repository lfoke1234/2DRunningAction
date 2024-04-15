using UnityEngine;

public class PlayerSlidingState : PlayerState
{
    private bool getKey;
    protected Vector2 slidingSize;
    protected Vector2 slidingOffset;
    protected Vector2 originalSize;
    protected Vector2 originalOffset;
    public PlayerSlidingState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        originalSize = player.cd.size;
        originalOffset = player.cd.offset;
        slidingSize = new Vector2(0.75f, 0.8f);
        slidingOffset = new Vector2(0, -0.8f);

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

        //if (triggerCalled)
        //{
        //    stateMachine.ChangeState(player.sIdleState);
        //}

        if (player.SlidButton() == false)
        {
            getKey = true;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            getKey = true;
        }

        if (getKey == true && player.CanStanding() == false)
            stateMachine.ChangeState(player.runState);
    }
}
