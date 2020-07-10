using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] SnakeHeadTrigger leftTrigger;
    [SerializeField] SnakeHeadTrigger rightTrigger;
    [SerializeField] GameObject hitBox;
    [SerializeField] GameObject weakPoint;
    [SerializeField] float dazzedTime = 1f;
    [SerializeField] GameObject egg;
    Animator headAnimator;
    AnimatorClipInfo[] m_clipInfo;
    float m_clip_length;
    bool isAttacking = false;
    bool playerHit = false;

    // Start is called before the first frame update
    void Start()
    {
        headAnimator = GetComponent<Animator>();      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator attack()
    {
        headAnimator.SetBool("AttackPlayer", true);
        
        yield return new WaitForSeconds(.5f);
        hitBox.SetActive(true);
        headAnimator.speed = 0;
        yield return new WaitForSeconds(.25f);
        hitBox.SetActive(false);
        if (!playerHit)
        {
            weakPoint.SetActive(true);
            yield return new WaitForSeconds(dazzedTime);
        }
        playerHit = false;
        headAnimator.speed = 1;
        weakPoint.SetActive(false);
        headAnimator.SetBool("AttackPlayer", false);
    }

    public void playerLeft()
    {
        if (!isAttacking)
        {
            if (transform.localScale.x < 0)
            {
                print("Change to left");
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                leftTrigger.isLeft = true;
                rightTrigger.isLeft = false;
            }
            StartCoroutine(attack());
        }
    }

    public void playerRight()
    {
        if (!isAttacking)
        {
            if (transform.localScale.x > 0)
            {
                print("Change to right");
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                leftTrigger.isLeft = false;
                rightTrigger.isLeft = true;
            }
            StartCoroutine(attack());
        }
    }

    public void hitPlayer()
    {
        playerHit = true;
    }

    public void dead()
    {
        Instantiate(egg);
        Destroy(gameObject);
    }
}
