using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            MovingTrap[] children = GetComponentsInChildren<MovingTrap>();

            foreach (MovingTrap child in children)
            {
                child.canMove = true;
            }
        }
    }
}
