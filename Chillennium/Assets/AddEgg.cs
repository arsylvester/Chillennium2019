using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AddEgg : MonoBehaviour
{
    PlayerAnimation m_p_anim;
    BoxCollider2D m_col;
    SpriteRenderer m_renderer;
    [SerializeField] GameObject[] stuff_to_destroy;
    [SerializeField] Health player_health;

    private void Awake()
    {
        m_col = GetComponent<BoxCollider2D>();
        m_renderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_p_anim = FindObjectOfType<PlayerAnimation>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_p_anim.numEggs++;
            m_p_anim.hasSwitched = false;
            m_col.enabled = false;
            m_renderer.enabled = false;
            DestroyStuff();
            player_health.Heal();
        }
    }

    void DestroyStuff()
    {
        foreach (GameObject obj in stuff_to_destroy)
        {
            Destroy(obj);
        }
    }
}
