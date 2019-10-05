using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    
    public float getHealthPercent()
    {
        return health / maxHealth;
    }

    public void takeDamge(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            SendMessageUpwards("dead");
        }
    }
}
