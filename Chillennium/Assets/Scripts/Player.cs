using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float dashSpeed = 5f;
    [SerializeField] float dashLength = .25f;
    [SerializeField] float dashCoolDown = .1f;
    [SerializeField] float knockbackDamping = .1f;
    public int damageDealt = 1;
    [SerializeField] BoxCollider2D HBoxUp;
    [SerializeField] BoxCollider2D HBoxDown;
    [SerializeField] BoxCollider2D HBoxLeft;
    [SerializeField] BoxCollider2D HBoxRight;
    public enum Direction {Up, Down, Left, Right}
    public Direction direct = Direction.Right;
    private bool isAttacking = false;
    public bool dashing = false;
    private bool canDash = true;
    private float timeDashStarted = 0;
    private float dashX;
    private float dashY;
    public bool slowed = false;
    private float lastMovedX = 0;
    private float lastMovedY = 0;
    private Rigidbody2D rb;

    float angle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        if (!dashing)
        {
            transform.position += Vector3.right * speed * xAxis * Time.deltaTime;
            transform.position += Vector3.up * speed * yAxis * Time.deltaTime;
            if(Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
            {
                lastMovedX = xAxis;
                lastMovedY = yAxis;
            }
        }
        if(Mathf.Abs(xAxis) > Mathf.Abs(yAxis))
        {
            if(xAxis > 0)
            {
                direct = Direction.Right;
            }
            else if(xAxis < 0)
            {
                direct = Direction.Left;
            }
        }
        else
        {
            if (yAxis > 0)
            {
                direct = Direction.Up;
            }
            else if (yAxis < 0)
            {
                direct = Direction.Down;
            }
        }

        //Attacking
        if (Input.GetButtonDown("Attack") && !isAttacking)
        {
            print("Attacking");
            switch (direct)
            {
                case Direction.Up:
                    StartCoroutine(hitBoxActivate(HBoxUp));
                    break;
                case Direction.Down:
                    StartCoroutine(hitBoxActivate(HBoxDown));
                    break;
                case Direction.Left:
                    StartCoroutine(hitBoxActivate(HBoxLeft));
                    break;
                case Direction.Right:
                    StartCoroutine(hitBoxActivate(HBoxRight));
                    break;
                default:
                    break;
            }
        }
        
        //Dashing
        if(Input.GetButtonDown("Dash") && canDash)
        {
            dashing = true;
            canDash = false;
            dashX = lastMovedX;
            dashY = lastMovedY;
            timeDashStarted = Time.time;
        }
        if(dashing && Time.time - dashLength < timeDashStarted)
        {
            Vector3 normalVector = Vector3.Normalize(new Vector3(dashX, dashY, 0));
            transform.position += Vector3.right * dashSpeed * normalVector.x * Time.deltaTime;
            transform.position += Vector3.up * dashSpeed * normalVector.y * Time.deltaTime;
        }
        else
        {
            dashing = false;
        }
        if(Time.time - dashLength - dashCoolDown > timeDashStarted && !slowed)
        {
            canDash = true;
        }
        
        if(Mathf.Abs(rb.velocity.x) > 0f || Mathf.Abs(rb.velocity.y) > 0f)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, knockbackDamping);
            //rb.velocity -= Vector2.one * 10 *Time.deltaTime;
        }
    }

    IEnumerator hitBoxActivate(BoxCollider2D hitBox)
    {
        hitBox.enabled = true;
        isAttacking = true;
        yield return new WaitForSecondsRealtime(.25f);
        hitBox.enabled = false;
        isAttacking = false;
    }

    public void dead()
    {
        print("YOU DIED");
    }

    public IEnumerator slowPlayer(float slowTime, float speedReduction)
    {
        print("Player Slowed");
        float oldSpeed = speed;
        speed *= speedReduction;
        canDash = false;
        slowed = true;
        yield return new WaitForSecondsRealtime(slowTime);
        speed = oldSpeed;
        canDash = true;
        slowed = false;
    }

    /*
    void OnGUI()
    {
        //Output the angle found above
        Vector2 screenPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 relativeMousePos = new Vector2(mousePos.x - screenPlayerPos.x, mousePos.y - screenPlayerPos.y);

        GUI.Label(new Rect(25, 20, 300, 40), "Player Position: " + screenPlayerPos.ToString());
        GUI.Label(new Rect(25, 60, 300, 40), "Mouse Position: " + mousePos.ToString());


        angle = Mathf.Rad2Deg * Mathf.Atan((mousePos.y - screenPlayerPos.y) / (mousePos.x - screenPlayerPos.x));

        if (mousePos.x < screenPlayerPos.x)
        {
            if (mousePos.y > screenPlayerPos.y)
            {
                angle += 180;
            }
            else
            {
                angle -= 180;
            }

        }



        GUI.Label(new Rect(25, 100, 300, 40), "Angle Between Objects: " + angle.ToString());
    }*/
}
