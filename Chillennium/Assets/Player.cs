using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float dashSpeed = 5f;
    [SerializeField] float dashLength = .25f;
    [SerializeField] float dashCoolDown = .1f;
    [SerializeField] BoxCollider2D HBoxUp;
    [SerializeField] BoxCollider2D HBoxDown;
    [SerializeField] BoxCollider2D HBoxLeft;
    [SerializeField] BoxCollider2D HBoxRight;
    public enum Direction {Up, Down, Left, Right}
    public Direction direct = Direction.Right;
    private bool isAttacking = false;
    private bool dashing = false;
    private bool canDash = true;
    private float timeDashStarted = 0;
    private float dashX;
    private float dashY;

    float angle;

    // Start is called before the first frame update
    void Start()
    {
        
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
            dashX = xAxis;
            dashY = yAxis;
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
        if(Time.time - dashLength - dashCoolDown > timeDashStarted)
        {
            canDash = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
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

    IEnumerator dash(float x, float y)
    {
        transform.position += Vector3.right * speed * x * Time.deltaTime;
        yield return new WaitForSeconds(.25f);
    }

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
    }
}
