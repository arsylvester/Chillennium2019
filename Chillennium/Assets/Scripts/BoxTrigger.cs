using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxTrigger : MonoBehaviour
{
    BoxCollider2D m_col;
    private bool collided;
    private int num_collided = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Rock"))
        {
            collided = true;
            num_collided += 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Rock"))
        {
            num_collided -= 1;
            if (num_collided == 0)
            {
                collided = false;
            }
        }
    }

    public bool getCollided()
    {
        return collided;
    }
}
