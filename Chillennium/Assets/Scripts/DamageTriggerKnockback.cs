using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerKnockback : MonoBehaviour
{
    BoxCollider2D damage_trigger;
    [SerializeField] int damage_amt;
    [SerializeField] float knockback_str;
    [SerializeField] Transform knockback_origin;
    [Tooltip("The tags that can be hurt by this trigger")]
    [SerializeField] List<string> damage_tags;

    private void Awake()
    {
        damage_trigger = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("entered");
        bool can_hurt = false;
        foreach (string tag in damage_tags)
        {
            if (tag == collision.tag)
            {
                can_hurt = true;
                Debug.Log("can hurt is true");
                break;
            }
        }
        if (can_hurt)
        {
            Health health_script = collision.GetComponentInParent<Health>();
            print("health script is: " + health_script);
            Vector3 knockback_dir;
            if (knockback_origin != null)
            {
                knockback_dir = Vector3.Normalize(collision.transform.position - knockback_origin.position);
            }
            else
            {
                knockback_dir = Vector3.Normalize(collision.transform.position - transform.position);
            }
            health_script.takeDamage(damage_amt, knockback_str, knockback_dir);
        }
    }
}
