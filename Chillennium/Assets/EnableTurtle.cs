using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnableTurtle : MonoBehaviour
{
    [SerializeField] GameObject[] m_objs_to_enable;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject obj in m_objs_to_enable)
            {
                obj.SetActive(true);
            }
        }
    }
}
