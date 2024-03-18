using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Move Info")]
    public float moveSpeed;

    [Header("JumpInfo")]
    public float jumpPower;
    public float secondJumpPower;
    public bool hasJump;
    public bool hasSecondJump;

    #region State
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerSecondJumpState secondJumpState { get; private set; }
    public PlayerSlidingState slidingState { get; private set; }
    #endregion

    protected override void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        runState = new PlayerRunState(this, stateMachine, "Run");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        secondJumpState = new PlayerSecondJumpState(this, stateMachine, "SJump");
        slidingState = new PlayerSlidingState(this, stateMachine, "Sliding");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialized(runState);
    }

    protected override void Update()
    {
        base .Update();

        stateMachine.currentState.Update();
    }

    protected override void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }
}
