using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collided with " + collision);
        Health target = collision.GetComponent<Health>();
        if (target)
        {
            print("hit target");
            target.takeDamage(GetComponentInParent<Player>().damageDealt);
        }
    }

}
