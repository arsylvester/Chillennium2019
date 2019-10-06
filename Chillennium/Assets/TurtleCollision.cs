using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleCollision : MonoBehaviour
{
    private Turtle turtle;

    private void Start()
    {
        turtle = GetComponentInParent<Turtle>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            turtle.hitRock(collision.transform);
        }
        else if(collision.gameObject.CompareTag("Spikes"))
        {
            turtle.hitSpikes();
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            SendMessageUpwards("hitRock");
        }
        else if (collision.gameObject.CompareTag("Spikes"))
        {
            SendMessageUpwards("hitSpikes");
        }
    }*/
}
