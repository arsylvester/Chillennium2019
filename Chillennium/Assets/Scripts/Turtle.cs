using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField] float approachPlayerSpeed;
    [SerializeField] GameObject attackDetection;
    [SerializeField] float strikeDelay = .5f;
    [SerializeField] float timeBetweenAttacks = 2f;
    //[SerializeField] GameObject normalBody;
    [SerializeField] GameObject attackBox;
    [SerializeField] GameObject weakPoint;
    private bool attacking = false;
    private Player player;
    private Health hp;


    void Start()
    {
        player = FindObjectOfType<Player>();
        attacking = false;
        hp = GetComponent<Health>();
        StartCoroutine(approachPlayer());
    }

    
    void Update()
    {
        
    }

    public void attack()
    {
        attacking = true;
        //StartCoroutine(attackPlayer());
    }

    IEnumerator approachPlayer() //Oh, your approaching me?
    {
        while (!attacking)
        {
            Vector3 differenceVector = player.transform.position - attackDetection.transform.position;
            transform.position += differenceVector * approachPlayerSpeed * Time.deltaTime;
            //transform.position = Vector3.Lerp(transform.position, player.transform.position, approachPlayerSpeed * .01f);
            yield return new WaitForEndOfFrame();
        }
    }
}
