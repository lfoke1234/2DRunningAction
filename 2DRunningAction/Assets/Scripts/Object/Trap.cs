using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private bool isBig;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            if (isBig)
            {
                player.PlayerDamaged(true);
            }
            else if (!isBig)
            {
                player.PlayerDamaged(false);
            }

        }
    }
}
