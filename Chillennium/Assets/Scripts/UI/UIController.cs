using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Image[] m_playerHealth;
    [SerializeField] Sprite[] m_playerHealthSprites;
    [SerializeField] Image[] m_bossHealth;
    [SerializeField] Sprite[] m_bossHealthSprites;
    [SerializeField] Health boss;
    private Player player;
    private int player_max;
    private int player_current;
    private bool dialogOpen = false;
    private GameObject dboxOpened;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        player_max = player.GetComponent<Health>().getMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        player_current = player.GetComponent<Health>().getHealth();
        if (player_current < 0)
        {
            player_current = 0;
        }
        UpdatePlayerUI();
        UpdateBossUI();

        if(dialogOpen && Input.GetButtonDown("Interact"))
        {
            closedbox();
        }
    }
    
    void UpdatePlayerUI()
    {
        int damage_taken = player_max - player_current;
        int mod = 1;
        for (int i = 0; i < damage_taken; i++)
        {
            int index_to_modify = (int)Mathf.Ceil(i / 2);
            if (mod == 1)
            {
                m_playerHealth[index_to_modify].sprite = m_playerHealthSprites[1];
                mod = 2;
            }
            else
            {
                m_playerHealth[index_to_modify].sprite = m_playerHealthSprites[mod];
                mod = 1;
            }
        }
    }

    void UpdateBossUI()
    {
        int damage_taken = boss.getMaxHealth() - boss.getHealth();
        for (int i = 0; i < m_bossHealth.Length; i++)
        {
            if (damage_taken > 0)
            {
                if (damage_taken > 1)
                {
                    m_bossHealth[i].sprite = m_bossHealthSprites[2];
                    damage_taken -= 2;
                }
                else
                {
                    m_bossHealth[i].sprite = m_bossHealthSprites[1];
                    damage_taken -= 1;
                }
            }
            else
            {
                m_bossHealth[i].sprite = m_bossHealthSprites[0];
            }
        }
    }

    public void changeBossSprites(Sprite[] boss_sprites)
    {
        m_bossHealthSprites = boss_sprites;
    }

    public void setBossUIEnabled(bool state)
    {
        foreach (Image boss in m_bossHealth)
        {
            boss.enabled = state;
        }
    }

    public void changeBossImages(Image[] newhealth)
    {
        m_bossHealth = newhealth;
    }

    public void setNewBossHealth(Health newBoss)
    {
        boss = newBoss;
    }

    public void openDialog(GameObject dbox)
    {
        dbox.SetActive(true);
        dboxOpened = dbox;
        dialogOpen = true;
    }

    public void closedbox()
    {
        dboxOpened.SetActive(false);
        dialogOpen = false;
    }
}
