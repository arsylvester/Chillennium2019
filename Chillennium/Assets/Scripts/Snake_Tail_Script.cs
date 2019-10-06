using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_Tail_Script : MonoBehaviour
{
    [SerializeField] float detection_radius;
    [SerializeField] float wait_time;
    [SerializeField] Animator tail_anim;
    [SerializeField] float attack_time = 0.3f;
    [SerializeField] Transform detection_point;
    float attack_timer;
    float wait_timer;
    [SerializeField]
    [ReadOnly]
    float idle_timer;
    AnimatorClipInfo[] m_clipInfo;
    float m_clip_length;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (distance_to_player() < detection_radius)
        {
            tail_anim.SetBool("Player_Too_Far", false);
            wait_timer = wait_time;
            tail_anim.SetFloat("Move_Timer", wait_timer);
        }
        else
        {
            tail_anim.SetBool("Player_Too_Far", true);
        }

        if (wait_timer >= 0)
        {
            wait_timer -= Time.deltaTime;
            tail_anim.SetFloat("Move_Timer", wait_timer);
            if (wait_timer == 0)
            {
                tail_anim.SetFloat("Move_Timer", -0.1f);
            }
        }

        if (idle_timer > 0)
        {
            idle_timer -= Time.deltaTime;
        }
        else
        {
            m_clipInfo = tail_anim.GetCurrentAnimatorClipInfo(0);
            if (m_clipInfo.Length > 0)
            {
                string clip_name = m_clipInfo[0].clip.name;
                if (clip_name == "snake_tail_swipe")
                {
                    attack_timer = attack_time;
                    tail_anim.SetFloat("Attack_Timer", attack_time);
                }
            }
            
        }

        if (attack_timer > 0)
        {
            attack_timer -= Time.deltaTime;
            tail_anim.SetFloat("Attack_Timer", attack_timer);
        }
        else
        {
            m_clipInfo = tail_anim.GetCurrentAnimatorClipInfo(0);
            if (m_clipInfo.Length > 0)
            {
                string clip_name = m_clipInfo[0].clip.name;
                if (clip_name == "snake_tail_swipe")
                {
                    m_clip_length = m_clipInfo[0].clip.length;
                    if (idle_timer <= 0)
                    {
                        idle_timer = m_clip_length;
                    }
                }
            }
            
        }

        if (player.transform.position.x < detection_point.position.x)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else if (player.transform.position.x > detection_point.position.x)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

    }

    private float distance_to_player()
    {
        return Vector2.Distance(player.transform.position, detection_point.position);
    }
}
