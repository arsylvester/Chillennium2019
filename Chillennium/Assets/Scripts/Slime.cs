using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] float slowLength = 3f;
    [SerializeField] float slowReduction = .5f;
    [SerializeField] float despawnRate = 1f;

    private float startTime;
    private SpriteRenderer sprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player)
        {
            if (!player.dashing && !player.slowed)
            {
                player.StartCoroutine(player.slowPlayer(3f, .5f));
            }
        }
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startTime = Time.time;
    }

    private void Update()
    {
        Color currentColor = sprite.color;
        sprite.color = new Color(currentColor.r, currentColor.g, currentColor.b, despawnRate/(Time.time - startTime + despawnRate));
        if(startTime + despawnRate < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
