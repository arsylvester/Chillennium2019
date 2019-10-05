using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    
    public int getHealthPercent()
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
