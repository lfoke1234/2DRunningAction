using UnityEngine;


public class Entity : MonoBehaviour
{
    #region Component
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    #endregion

    [Header ("Collision info")]
    [SerializeField] private LayerMask isGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    public void SetVelocity(float _xVeolcity, float _yVeolcity)
    {
        rb.velocity = new Vector2(_xVeolcity, _yVeolcity);
    }

    public void SetZeroVeolcity()
    {
        rb.velocity = new Vector2(0, 0);
    }

    #region Check Layer

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, isGround);

    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

    }
}
