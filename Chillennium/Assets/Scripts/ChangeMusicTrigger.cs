using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class ChangeMusicTrigger : MonoBehaviour
{
    BoxCollider2D m_col;
    AudioSource m_source;

    private void Awake()
    {
        m_col = GetComponent<BoxCollider2D>();
        m_source = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.playTrack(m_source.clip);
        }
    }
}
