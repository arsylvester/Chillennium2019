using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    [SerializeField]
    private float m_max_health;
    [SerializeField]
    [ReadOnly]
    private float m_current_health;
    [SerializeField]
    [Tooltip("Seconds until object is killed")]
    private float m_kill_delay = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_current_health = m_max_health;
    }

    public float GetCurrentHealth()
    {
        return m_current_health;
    }

    public float GetMaxHealth()
    {
        return m_max_health;
    }

    /// <summary>
    /// reduces the health of the object by the amount specified.
    /// capped below at zero health.
    /// </summary>
    /// <param name="amount"> amount to hurt the objbect</param>
    public void Hurt(float amount)
    {
        m_current_health -= amount;
        if (m_current_health < 0)
        {
            m_current_health = 0f;
        }
        if (m_current_health == 0)
        {
            IKillable killable;
            killable = GetComponent<IKillable>();
            if (killable != null)
            {
                killable.Kill(m_kill_delay);
            }
        }
    }

    /// <summary>
    /// increases the health of the object by the amount specified.
    /// won't increase past max health.
    /// </summary>
    /// <param name="amount"></param>
    public void Heal(float amount)
    {
        m_current_health += amount;
        if (m_current_health > m_max_health)
        {
            m_current_health = m_max_health;
        }
    }
}
