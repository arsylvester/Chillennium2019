using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : MonoBehaviour
{
    [SerializeField] Transform featherPointOne;
    [SerializeField] Transform featherPointTwo;
    [SerializeField] float flyBackSpeed;
    [SerializeField] GameObject feather;
    [SerializeField] Transform throwFeatherSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(featherAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator featherAttack()
    {
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
        yield return new WaitForSecondsRealtime(.5f);
        for(int x = -30; x <= 30; x += 15)
        {
            Instantiate(feather, throwFeatherSpawn.position, Quaternion.Euler(0,0,x));
        }
        
    }

    public void attack()
    {
        
    }
}
