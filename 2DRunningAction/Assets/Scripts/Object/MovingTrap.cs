using UnityEngine;

public class MovingTrap : Trap
{
    public bool canMove;

    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform[] movePoint;
    [SerializeField] private bool canRotate;

    private int i;


    private void Start()
    {


    }

    private void Update()
    {
        if (canMove == false)
            return;

        transform.position = Vector3.MoveTowards(transform.position, movePoint[i].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoint[i].position) < 0.25f)
        {
            i++;

            if (i >= movePoint.Length)
                i = 0;
        }

        if (canRotate == true)
        {
            if (transform.position.x > movePoint[i].position.x)
            {
                transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
            }
        }
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
