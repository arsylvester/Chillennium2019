using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float turnCoolDown;
    [SerializeField] GameObject horizontalAlignment;
    [SerializeField] GameObject verticalAlignment;
    private Player player;
    private float timeOfLastTurn;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - turnCoolDown > timeOfLastTurn)
        {
            Vector2 displacemnetVector = player.transform.position - transform.position;
            if (Mathf.Abs(displacemnetVector.x) > Mathf.Abs(displacemnetVector.y))
            {
                verticalAlignment.SetActive(false);
                horizontalAlignment.SetActive(true);
                if (displacemnetVector.x > 0)
                    transform.position += Vector3.right * speed * Time.deltaTime;
                else
                    transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                verticalAlignment.SetActive(true);
                horizontalAlignment.SetActive(false);
                if (displacemnetVector.y > 0)
                    transform.position += Vector3.up * speed * Time.deltaTime;
                else
                    transform.position += Vector3.down * speed * Time.deltaTime;
            }
            timeOfLastTurn = Time.time;
        }
    }
}
