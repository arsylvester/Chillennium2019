using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxTrigger : MonoBehaviour
{
    BoxCollider2D m_col;
    private bool collided;

    public void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Rock"))
        {
            collided = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        collided = false;
    }

    public bool getCollided()
    {
        return collided;
    }
}
