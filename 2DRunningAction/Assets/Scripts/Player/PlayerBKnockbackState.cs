using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBKnockbackState : PlayerState
{
    private bool isKnocked;

    public PlayerBKnockbackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.TakeDamage(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.IsGroundDetected() == false)
        {
            isKnocked = true;
        }

        if (player.IsGroundDetected() && isKnocked)
        {
            player.SetZeroVeolcity();
            isKnocked = false;
        }
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.runState);
        }
    }
}
