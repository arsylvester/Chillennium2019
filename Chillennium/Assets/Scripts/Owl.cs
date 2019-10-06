using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : MonoBehaviour
{
    [SerializeField] Transform featherPointOne;
    [SerializeField] Transform featherPointTwo;
    [SerializeField] float flyBackSpeed;
    [SerializeField] float approachPlayerSpeed;
    [SerializeField] GameObject feather;
    [SerializeField] Transform throwFeatherSpawn;
    [SerializeField] GameObject attackDetection;
    [SerializeField] float strikeDelay = .5f;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] float featherThrowDelay = .1f;
    [SerializeField] float dazedTime = 2f;
    [SerializeField] GameObject normalBody;
    [SerializeField] GameObject attackBody;
    [SerializeField] GameObject attackBox;
    [SerializeField] GameObject weakPoint;
    [SerializeField] GameObject owlDown;
    [SerializeField] GameObject owlUp;
    [SerializeField] GameObject owl50;
    private bool inPitFall = false;
    private bool attacking = false;
    private Player player;
    private Health hp;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        attacking = false;
        hp = GetComponent<Health>();
        StartCoroutine(featherAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator featherAttack()
    {
        attackDetection.SetActive(false);
        owlDown.SetActive(false);
        owlUp.SetActive(true);
        owlUp.GetComponent<Animator>().SetBool("Flap", true);
        Vector3 pointToAttackFrom = new Vector3(Random.Range(featherPointOne.position.x, featherPointTwo.position.x), Random.Range(featherPointOne.position.y, featherPointTwo.position.y), 0);
        float travelDistance = Vector3.Distance(transform.position, pointToAttackFrom);
        float startTime = Time.time;
        while (transform.position != pointToAttackFrom)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * flyBackSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / travelDistance;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(transform.position, pointToAttackFrom, fractionOfJourney);
            yield return new WaitForEndOfFrame();
        }
        owlUp.SetActive(false);
        owl50.SetActive(true);
        yield return new WaitForSecondsRealtime(featherThrowDelay);
        for(int x = -30; x <= 30; x += 15)
        {
            Instantiate(feather, throwFeatherSpawn.position, Quaternion.Euler(0,0,x));
        }
        attackDetection.SetActive(true);
        owl50.SetActive(false);
        StartCoroutine(approachPlayer());
    }

    IEnumerator approachPlayer() //Oh, you approach me?
    {
        owlDown.SetActive(true);
        while (!attacking)
        {
            Vector3 differenceVector = player.transform.position - attackDetection.transform.position;
            transform.position += differenceVector * approachPlayerSpeed * Time.deltaTime; 
            //transform.position = Vector3.Lerp(transform.position, player.transform.position, approachPlayerSpeed * .01f);
            yield return new WaitForEndOfFrame();
        }
    }

    public void attack()
    {
        attacking = true;
        StartCoroutine(attackPlayer());
    }

    IEnumerator attackPlayer()
    {
        normalBody.SetActive(false);
        attackDetection.SetActive(false);
        attackBody.SetActive(true);
        owlDown.SetActive(false);
        owlUp.SetActive(true);
        owlUp.GetComponent<Animator>().SetBool("Flap", false);
        yield return new WaitForSecondsRealtime(strikeDelay);
        attackBox.SetActive(true);
        yield return new WaitForSecondsRealtime(.1f);
        attacking = false;
        attackBox.SetActive(false);
        owlDown.SetActive(true);
        owlUp.SetActive(false);
        if (inPitFall)
        {
            weakPoint.SetActive(true);
            float currentHp = hp.getHealthPercent();
            while (!thirdDamage(currentHp))
            {
                yield return new WaitForEndOfFrame();
            }
            weakPoint.SetActive(false);
            inPitFall = false;
        }
        normalBody.SetActive(true);
        attackBody.SetActive(false); 
        yield return new WaitForSeconds(timeBetweenAttacks);
        attackDetection.SetActive(true);
        StartCoroutine(featherAttack());
    }

    private bool thirdDamage(float currHP)
    {
        if(currHP - hp.getHealthPercent() >= .33f)
        {
            return true;
        }
        return false;
    }

    public void dead()
    {
        Application.LoadLevel("You Won");
    }

    public void hitPitFall()
    {
        print("Pitfall hit.");
        inPitFall = true;
    }
}
