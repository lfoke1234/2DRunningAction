using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Entity
{
    public static Player instance;

    public bool isDead;
    public bool isBKnock;
    public bool test;

    private Toggle toggle;

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

    public float playerMaxHealth;
    public float playerHealth;
    [SerializeField] private float blinkTime;

    [Header("Collision Check")]
    [SerializeField] private Transform climbCheck;
    [SerializeField] private float climbCheckDistance;

    [SerializeField] private Transform standingCheck;
    [SerializeField] private float standCheckDistance;

    public System.Action onHealthChanged;

    #region State
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerSecondJumpState secondJumpState { get; private set; }
    public PlayerSlidingState slidingState { get; private set; }
    public PlayerSlidingIdleState sIdleState { get; private set; }
    //public PlayerLedgeClimbState climbState { get; private set; }
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
        //climbState = new PlayerLedgeClimbState(this, stateMachine, "Climb");
        //wallMoveState = new PlayerWallMoveState(this, stateMachine, "WMove");
        knockbackState = new PlayerKnockbackState(this, stateMachine, "Knockback");
        bKncokbackState = new PlayerBKnockbackState(this, stateMachine, "BKnockback");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialized(runState);

        speedMilestone = milestoneIncreaser;

        defaultSpeed = moveSpeed;
        defaultMilestoneIncrease = milestoneIncreaser;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject toggleGameObject = GameObject.Find("Toggle");
        if (toggleGameObject != null)
        {
            toggle = toggleGameObject.GetComponent<Toggle>();
            if (toggle != null)
            {
                toggle.onValueChanged.AddListener(ToggleTest);
            }
            else
            {
                Debug.LogError("Toggle 컴포넌트가 게임 오브젝트에 없습니다.");
            }
        }
        else
        {
        }
    }

    protected override void Update()
    {
        base.Update();

        if (Time.timeScale == 0.0f)
            return;

        stateMachine.currentState.Update();
        SpeedController();


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

        if (transform.position.x > speedMilestone)
        {
            speedMilestone = speedMilestone + milestoneIncreaser;

            moveSpeed *= speedMultiplier;
            milestoneIncreaser = milestoneIncreaser * speedMultiplier;

            if (moveSpeed > maxSpeed)
                moveSpeed = maxSpeed;
        }

    }

    public void SpeedReset()
    {
        moveSpeed = defaultSpeed;
        milestoneIncreaser = defaultMilestoneIncrease;
    }
    #endregion

    #region Damage
    public void ToggleTest(bool toggleState)
    {
        test = toggleState;
    }

    public void Knockback(bool isBig)
    {
        isKnocked = true;

        if (!isBig)
        {
            rb.velocity = knockbackPower;
        }
        else if (isBig)
        {
            rb.velocity = bKnockbackPower;
        }
    }

    public void TakeDamage(bool isBig)
    {
        SpeedReset();
        Knockback(isBig);

        if (!isBig)
        {
            playerHealth -= 1;
            blinkTime = 2;
        }
        else
        {
            playerHealth -= 2;
            blinkTime = 3.5f;
        }

        if (onHealthChanged != null)
            onHealthChanged();

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

    public void CancelKnockback() => isKnocked = false;

    public void PlayerDamaged(bool isBig)
    {
        if (isKnocked || test || isDead)
            return;

        if (playerHealth <= 0)
        {
            Die();
            return;
        }

        AudioManager.instance.PlaySFX(2);

        if (isBig)
        {
            stateMachine.ChangeState(bKncokbackState);
        }
        else if (!isBig)
        {
            stateMachine.ChangeState(knockbackState);
        }
    }

    public void ResetPlayer()
    {
        stateMachine.ChangeState(runState);
    }

    public void Die()
    {
        stateMachine.ChangeState(deadState);
    }
    #endregion

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(climbCheck.position, new Vector3(climbCheck.position.x + climbCheckDistance, climbCheck.position.y));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(standingCheck.position, new Vector3(standingCheck.position.x, standingCheck.position.y + standCheckDistance));
    }
}
