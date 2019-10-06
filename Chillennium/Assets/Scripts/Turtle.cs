using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField] float approachPlayerSpeed;
    [SerializeField] GameObject attackDetection;
    [SerializeField] float swipeDelay = .5f;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] float swipeDuration = 1f;
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float spinAttackSpeed = 5f;
    [SerializeField] float dazedTime = 2f;
    //[SerializeField] GameObject normalBody;
    [SerializeField] GameObject attackBox;
    [SerializeField] GameObject weakPoint;
    [SerializeField] GameObject spinHitBox;
    private bool attacking = false;
    private Player player;
    private Health hp;
    private Animator animator;
    private bool phase2 = false;
    private bool hitSpike = false;
    private bool rockHit = false;
    private Transform rock;


    void Start()
    {
        player = FindObjectOfType<Player>();
        attacking = false;
        hp = GetComponent<Health>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(approachPlayer());
    }

    
    void Update()
    {
        if(hp.getHealthPercent() <= .50f && !phase2)
        {
            phase2 = true;
            print("Phase 2");
            animator.SetBool("Attack", false);
            attackBox.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(spinAttack());
        }
    }

    public void attack()
    {
        if (!phase2)
        {
            attacking = true;
            StartCoroutine(attackPlayer());
        }
    }

    IEnumerator approachPlayer() //Oh, your approaching me?
    {
        print("approaching");
        while (!attacking)
        {
            Vector3 differenceVector = player.transform.position - transform.position;
            transform.up = Vector2.Lerp(transform.up, -differenceVector, turnSpeed * Time.deltaTime);
            //transform.position += Vector3.up * -approachPlayerSpeed * Time.deltaTime;
            transform.position += -transform.up * approachPlayerSpeed * Time.deltaTime;
            //transform.position = Vector3.Lerp(transform.position, player.transform.position, approachPlayerSpeed * .01f);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator attackPlayer()
    {
        print("attacking");
        animator.SetBool("Attack", true);
        yield return new WaitForSecondsRealtime(swipeDelay);
        attackBox.SetActive(true);
        yield return new WaitForSecondsRealtime(swipeDuration);
        animator.SetBool("Attack", false);
        attackBox.SetActive(false);
        attacking = false;
        StartCoroutine(approachPlayer());
    }

    IEnumerator spinAttack()
    {
        print("spin attack");
        rockHit = false;
        Vector3 differenceVector = player.transform.position - transform.position;
        animator.SetBool("Spin", true);
        weakPoint.SetActive(false);
        spinHitBox.SetActive(true);
        yield return new WaitForSecondsRealtime(.5f);
        while (!hitSpike)
        {
            transform.position += differenceVector.normalized * spinAttackSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if(rockHit)
            {
                //float angle = Mathf.Rad2Deg * Mathf.Atan((rock.position.y - transform.position.y) / (rock.position.x - transform.position.x));
                differenceVector = player.transform.position - transform.position;
                rockHit = false;
            }
        }
        transform.up = differenceVector;
        animator.SetBool("Spin", false);
        hitSpike = false;
        spinHitBox.SetActive(false);
        StartCoroutine(dazed());
    }

    IEnumerator dazed()
    {
        print("dazed");
        animator.SetBool("Stunned", true);
        weakPoint.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        animator.SetBool("Stunned", false);
        yield return new WaitForSecondsRealtime(dazedTime);     
        StartCoroutine(spinAttack());
    }

    public void hitRock(Transform rockTrans)
    {
        print("Hit rocks");
        rockHit = true;
        rock = rockTrans;
    }
    
    public void hitSpikes()
    {
        print("Hit Spikes");
        hitSpike = true;
    }


    public void dead()
    {
        Destroy(gameObject);
    }
}
