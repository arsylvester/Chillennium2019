using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(BoxCollider2D))]
public class Change_Boss_UI : MonoBehaviour
{
    [SerializeField]
    Image[] boss_images;
    [SerializeField]
    Sprite[] boss_sprites;
    [SerializeField]
    Health m_newBoss;
    [SerializeField]
    bool changeImages;
    [SerializeField]
    bool change_sprites;
    [SerializeField]
    bool disable_boss_UI = false;
    [SerializeField]
    bool change_bosses;
    BoxCollider2D m_col;
    UIController m_ui;

    private void Awake()
    {
        m_col = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        m_ui = FindObjectOfType<UIController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("collided");
        if (collision.CompareTag("Player"))
        {
            applyChanges();
        }
    }

    void applyChanges()
    {
        if (changeImages)
        {
            m_ui.changeBossImages(boss_images);
        }
        if (change_sprites)
        {
            m_ui.changeBossSprites(boss_sprites);
        }
        m_ui.setBossUIEnabled(!disable_boss_UI);
        if (change_bosses)
        {
            m_ui.setNewBossHealth(m_newBoss);
        }
    }
}
