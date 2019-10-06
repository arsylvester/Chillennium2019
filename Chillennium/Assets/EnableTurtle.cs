using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnableTurtle : MonoBehaviour
{
    [SerializeField] Turtle m_turtle;
    BoxCollider2D m_col;
    // Start is called before the first frame update
    void Awake()
    {
        m_col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_turtle.enabled = true;
        }
    }
}
