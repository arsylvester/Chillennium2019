using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private int health;
    [Tooltip("amount of time (in seconds) object is invincible for after being hit")]
    [SerializeField]
    float m_invincibility_time = 0f;
    private float m_invincibility_timer;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (m_invincibility_timer > 0)
        {
            m_invincibility_timer -= Time.deltaTime;
        }
    }

    public int getHealthPercent()
    {
        return health / maxHealth;
    }

    public void takeDamage(int damage, float knockback_str = 0f, Vector2 knockback_dir = new Vector2())
    {
        if (m_invincibility_timer <= 0)
        {
            health -= damage;
            if(health <= 0)
            {
                SendMessageUpwards("dead");
            }
            else
            {
                m_invincibility_timer = m_invincibility_time;
            }
            Rigidbody2D self_rb = GetComponent<Rigidbody2D>();
            if (self_rb)
            {
                self_rb.velocity = knockback_dir * knockback_str;
            }
        }
        
    }
}
