using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : PlayerState
{
    public PlayerKnockbackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Knockback(false);
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

        if (triggerCalled )
        {
            player.CancelKnockback();
            stateMachine.ChangeState(player.runState);
        }
    }
}
