using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BreakableRock : MonoBehaviour
{
    [SerializeField] float respawnDelay = 8f;
    [SerializeField] Sprite[] rockSprites;
    BoxCollider2D m_col;
    BoxTrigger m_boxTrigger;
    private float reactivation_timer = 0f;
    GameObject player;
    int spriteIndex = 0;

    public void Awake()
    {
        m_col = GetComponent<BoxCollider2D>();
        m_boxTrigger = GetComponentInChildren<BoxTrigger>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Start()
    {

    }

    public void Update()
    {
        if (reactivation_timer > 0)
        {
            reactivation_timer -= Time.deltaTime;
        }
        else
        {
            Reactivate();
        }
    }
    public void dead()
    {
        //change sprites
        //turn off collision
        m_col.enabled = false;
        spriteIndex = 1;
        changeSprite();
        respawn(respawnDelay);
    }

    /// <summary>
    /// play rock drop animation, reactivate collision
    /// </summary>
    /// <param name="reactivation_time"></param>
    public void respawn(float reactivation_time)
    {
        //play rock drop animation
        spriteIndex = 0;
        //set activation timer
        reactivation_timer = reactivation_time;
    }

    /// <summary>
    /// reactivate collision if rock is back and
    /// object is not inside rock collision
    /// </summary>
    private void Reactivate()
    {
       // if (m_boxTrigger.getCollided())
       // {
      //      reactivation_timer = 0.1f;
      //  }
       // else
        {
            m_col.enabled = true;
            changeSprite();
        }
    }

    void changeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = rockSprites[spriteIndex];
    }
}
