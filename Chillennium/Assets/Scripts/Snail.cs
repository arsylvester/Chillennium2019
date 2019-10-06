using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float turnCoolDown;
    [SerializeField] float slimeSpawnRate;
    [SerializeField] float strikeDelay = 2f;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] GameObject normalBody;
    [SerializeField] GameObject attackTrigger;
    [SerializeField] GameObject attackBody;
    [SerializeField] GameObject slimeSpawner;
    [SerializeField] GameObject slime;
    [SerializeField] GameObject attackBox;
    [SerializeField] Animator m_snail_anim;
    private Player player;
    private float timeOfLastTurn;
    private Player.Direction direction = Player.Direction.Down;
    private bool isAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(makeSlime());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            if (Time.time - turnCoolDown > timeOfLastTurn)
            {
                Vector2 displacemnetVector = player.transform.position - transform.position;
                if (Mathf.Abs(displacemnetVector.x) > Mathf.Abs(displacemnetVector.y))
                {

                    if (displacemnetVector.x > 0)
                    {
                        direction = Player.Direction.Right;
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                    else
                    {
                        direction = Player.Direction.Left;
                        transform.rotation = Quaternion.Euler(0, 0, -90);
                    }
                }
                else
                {
                    // horizontalAlignment.SetActive(false);
                    if (displacemnetVector.y > 0)
                    {
                        direction = Player.Direction.Up;
                        transform.rotation = Quaternion.Euler(0, 0, 180);
                    }
                    else
                    {
                        direction = Player.Direction.Down;
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                timeOfLastTurn = Time.time;
            }

            switch (direction)
            {
                case Player.Direction.Up:
                    transform.position += Vector3.up * speed * Time.deltaTime;
                    break;
                case Player.Direction.Down:
                    transform.position += Vector3.down * speed * Time.deltaTime;
                    break;
                case Player.Direction.Left:
                    transform.position += Vector3.left * speed * Time.deltaTime;
                    break;
                case Player.Direction.Right:
                    transform.position += Vector3.right * speed * Time.deltaTime;
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator makeSlime()
    {
        while (true)
        {
            if (!isAttacking)
            {
                Instantiate(slime, slimeSpawner.transform.position, slimeSpawner.transform.rotation);
            }
            yield return new WaitForSecondsRealtime(slimeSpawnRate);
        }
    }

    public void attack()
    {
        isAttacking = true;
        normalBody.SetActive(false);
        attackTrigger.SetActive(false);
        attackBody.SetActive(true);
        m_snail_anim.SetBool("IsAttacking", true);
        StartCoroutine(strike());
    }
    
    IEnumerator strike()
    {
        yield return new WaitForSecondsRealtime(strikeDelay);
        attackBox.SetActive(true);
        yield return new WaitForSecondsRealtime(.1f);
        isAttacking = false;
        normalBody.SetActive(true);
        attackBody.SetActive(false);
        attackBox.SetActive(false);
        m_snail_anim.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(timeBetweenAttacks);
        attackTrigger.SetActive(true);
    }

    public void dead()
    {
        Destroy(gameObject);
    }
}
