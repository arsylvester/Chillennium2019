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
    [SerializeField] GameObject normalBody;
    [SerializeField] GameObject attackBody;
    [SerializeField] GameObject attackBox;
    private bool attacking = false;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        attacking = false;
        StartCoroutine(featherAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator featherAttack()
    {
        attackDetection.SetActive(false);
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
        yield return new WaitForSecondsRealtime(featherThrowDelay);
        for(int x = -30; x <= 30; x += 15)
        {
            Instantiate(feather, throwFeatherSpawn.position, Quaternion.Euler(0,0,x));
        }
        attackDetection.SetActive(true);
        StartCoroutine(approachPlayer());
    }

    IEnumerator approachPlayer() //Oh, you appraoch me?
    {
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
        yield return new WaitForSecondsRealtime(strikeDelay);
        attackBox.SetActive(true);
        yield return new WaitForSecondsRealtime(.1f);
        attacking = false;
        normalBody.SetActive(true);
        attackBody.SetActive(false);
        attackBox.SetActive(false);
        yield return new WaitForSeconds(timeBetweenAttacks);
        attackDetection.SetActive(true);
        StartCoroutine(featherAttack());
    }

    public void dead()
    {

    }
}
