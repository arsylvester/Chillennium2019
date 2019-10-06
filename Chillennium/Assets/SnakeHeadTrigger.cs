using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadTrigger : MonoBehaviour
{
    public bool isLeft = true;
    public bool isHitBox = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player)
        {
            if (isHitBox)
            {
                GetComponentInParent<SnakeHead>().hitPlayer();
            }
            else
            {
                if (isLeft)
                    GetComponentInParent<SnakeHead>().playerLeft();
                else
                    GetComponentInParent<SnakeHead>().playerRight();
            }
        }
        
    }
}
