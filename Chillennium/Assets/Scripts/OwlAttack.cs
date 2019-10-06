using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PitFall pitfall = collision.GetComponent<PitFall>();
        if(pitfall)
        {
            SendMessageUpwards("hitPitFall");
            pitfall.wasHit();
        }
    }
}
