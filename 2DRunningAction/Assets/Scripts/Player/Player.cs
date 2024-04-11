using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    public bool isBKnock;
    public bool test;

    [Header("Move Info")]
    public float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedMultiplier;
    private float defaultSpeed;
    [Space]
    [SerializeField] private float milestoneIncreaser;
    private float defaultMilestoneIncrease;
    [SerializeField] private float speedMilestone;

    [Header("JumpInfo")]
    public float jumpPower;
    public float secondJumpPower;
    public bool hasJump;
    public bool hasSecondJump;
    public float fallPower;

    [SerializeField] private float playerHealth;
    [SerializeField] private float blinkTime;

    [Header("Collision Check")]
    [SerializeField] private Transform climbCheck;
    [SerializeField] private float climbCheckDistance;

    [SerializeField] private Transform standingCheck; 
    [SerializeField] private float standCheckDistance;

    #region State
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerSecondJumpState secondJumpState { get; private set; }
    public PlayerSlidingState slidingState { get; private set; }
    public PlayerSlidingIdleState sIdleState { get; private set; }
    public PlayerLedgeClimbState climbState { get; private set; }
    //public PlayerWallMoveState wallMoveState { get; private set; }
    public PlayerKnockbackState knockbackState { get; private set; }
    public PlayerBKnockbackState bKncokbackState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
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
        sIdleState = new PlayerSlidingIdleState(this, stateMachine, "SIdle");
        climbState = new PlayerLedgeClimbState(this, stateMachine, "Climb");
        //wallMoveState = new PlayerWallMoveState(this, stateMachine, "WMove");
        knockbackState = new PlayerKnockbackState(this, stateMachine, "Knockback");
        bKncokbackState = new PlayerBKnockbackState(this, stateMachine, "BKnockback");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialized(runState);

        speedMilestone = milestoneIncreaser;

        defaultSpeed = moveSpeed;
        defaultMilestoneIncrease = milestoneIncreaser;
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
        SpeedController();

        if (test)
            moveSpeed = 0;

        if (!test && Input.GetKeyDown(KeyCode.T))
        {
            test = true;
        }
        else if (test && Input.GetKeyDown(KeyCode.T))
        {
            test = false;
        }
        
    }

    protected override void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    #region Collision
    public virtual bool CanClimb() => Physics2D.Raycast(climbCheck.position, Vector2.right * facingDir, climbCheckDistance, isGround);

    public bool CanStanding() => Physics2D.Raycast(standingCheck.position, Vector2.up, standCheckDistance, isGround);
    #endregion

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    #region Speed control
    public void SpeedController()
    {
        if (moveSpeed == maxSpeed)
            return;

        if(transform.position.x > speedMilestone)
        {
            speedMilestone = speedMilestone + milestoneIncreaser;

            moveSpeed *= speedMultiplier;
            milestoneIncreaser = milestoneIncreaser * speedMultiplier;

            if (moveSpeed > maxSpeed)
                moveSpeed = maxSpeed;
        }

    }

    private void SpeedReset()
    {
        moveSpeed = defaultSpeed;
        milestoneIncreaser = defaultMilestoneIncrease;
    }
    #endregion

    #region Damage
    public void Knockback(bool isBig)
    {
        isKnocked = true;

        if (!isBig)
        {
            rb.velocity = knockbackPower;
        }
        else if(isBig)
        {
            rb.velocity = bKnockbackPower;
        }
    }

    public void TakeDamage(bool isBig)
    {
        SpeedReset();
        Knockback(isBig);

        if (!isBig)
            playerHealth -= 1;
        else
            playerHealth -= 2;

        StartCoroutine(BlinkSprite());
    }

    public IEnumerator BlinkSprite()
    {
        // TakeDamage(value);
        fx.InvokeRepeating("GrayColorBlink", 0, 0.1f);
        yield return new WaitForSeconds(blinkTime);
        isKnocked = false;
        fx.CancelColorChange();
        
    }
    #endregion

    public void CancelKnockback() => isKnocked = false;

    public void PlayerDamaged(bool isBig)
    {
        if (isKnocked)
            return;

        if (isBig)
        {
            stateMachine.ChangeState(bKncokbackState);
        }
        else if (!isBig)
        {
            stateMachine.ChangeState(knockbackState);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(climbCheck.position, new Vector3(climbCheck.position.x + climbCheckDistance, climbCheck.position.y));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(standingCheck.position, new Vector3(standingCheck.position.x, standingCheck.position.y + standCheckDistance));
    }
}
