using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] float slowLength = 3f;
    [SerializeField] float slowReduction = .5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player)
        {
            if (!player.dashing)
            {
                player.StartCoroutine(player.slowPlayer(3f, .5f));
            }
        }
    }
}
