﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Player hit " + collision);
        if (collision.CompareTag("WeakPoint"))// || collision.CompareTag("Rock"))
        {
            Health target = collision.GetComponentInParent<Health>();
            target.takeDamge(GetComponentInParent<Player>().damageDealt);
        }
    }

}
