using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public bool userInput;
    public bool pecking;
    public Player PlayerScript;

    private bool facingRight;
    public bool hasSwitched = false;
    private float xAxis;
    private float yAxis;

    private GameObject frontChick;
    private GameObject sideChick;
    private GameObject backChick;
    private Animator animFront;
    private Animator animSide;
    private Animator animBack;

    public int numEggs;

    [SerializeField] Animator noEggAnimFront;
    [SerializeField] Animator noEggAnimSide;
    [SerializeField] Animator noEggAnimBack;
    [SerializeField] Animator egg1AnimFront;
    [SerializeField] Animator egg1AnimSide;
    [SerializeField] Animator egg1AnimBack;
    [SerializeField] Animator egg2AnimFront;
    [SerializeField] Animator egg2AnimSide;
    [SerializeField] Animator egg2AnimBack;
    [SerializeField] Animator egg3AnimFront;
    [SerializeField] Animator egg3AnimSide;
    [SerializeField] Animator egg3AnimBack;
    [SerializeField] Animator egg4AnimFront;
    [SerializeField] Animator egg4AnimSide;
    [SerializeField] GameObject noEddFrontChick;
    [SerializeField] GameObject noEggSideChick;
    [SerializeField] GameObject noEggBackChick;
    [SerializeField] GameObject egg1FrontChick;
    [SerializeField] GameObject egg1SideChick;
    [SerializeField] GameObject egg1BackChick;
    [SerializeField] GameObject egg2FrontChick;
    [SerializeField] GameObject egg2SideChick;
    [SerializeField] GameObject egg2BackChick;
    [SerializeField] GameObject egg3FrontChick;
    [SerializeField] GameObject egg3SideChick;
    [SerializeField] GameObject egg3BackChick;
    [SerializeField] GameObject egg4FrontChick;
    [SerializeField] GameObject egg4SideChick;
    [SerializeField] ParticleSystem dashEffect;
    [SerializeField] float dashEffectDuration;

    // Start is called before the first frame update
    void Start()
    {
        frontChick = noEddFrontChick;
        sideChick = noEggSideChick;
        backChick = noEggBackChick;

        animFront = noEggAnimFront;
        animSide = noEggAnimSide;
        animBack = noEggAnimBack;

        if (!dashEffect.isStopped)
            dashEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            numEggs = 0;
            hasSwitched = false;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            numEggs = 1;
            hasSwitched = false;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            numEggs = 2;
            hasSwitched = false;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            numEggs = 3;
            hasSwitched = false;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            numEggs = 4;
            hasSwitched = false;
        }

        if(PlayerScript.isAttacking)
        {
            pecking = true;
        }
        else
        {
            pecking = false;
        }

        if (!hasSwitched)
        {
            hasSwitched = true;

            if (frontChick.activeSelf == true)
                frontChick.SetActive(false);
            if (sideChick.activeSelf == true)
                sideChick.SetActive(false);
            if (backChick.activeSelf == true)
                backChick.SetActive(false);

            switch (numEggs)
            {
                case 1:
                    frontChick = egg1FrontChick;
                    sideChick = egg1SideChick;
                    backChick = egg1BackChick;

                    animFront = egg1AnimFront;
                    animSide = egg1AnimSide;
                    animBack = egg1AnimBack;

                    break;

                case 2:
                    frontChick = egg2FrontChick;
                    sideChick = egg2SideChick;
                    backChick = egg2BackChick;

                    animFront = egg2AnimFront;
                    animSide = egg2AnimSide;
                    animBack = egg2AnimBack;

                    break;

                case 3:
                    frontChick = egg3FrontChick;
                    sideChick = egg3SideChick;
                    backChick = egg3BackChick;

                    animFront = egg3AnimFront;
                    animSide = egg3AnimSide;
                    animBack = egg3AnimBack;

                    break;

                case 4:
                    frontChick = egg4FrontChick;
                    sideChick = egg4SideChick;
                    backChick = egg3BackChick;

                    animFront = egg4AnimFront;
                    animSide = egg4AnimSide;
                    animBack = egg3AnimBack;

                    break;

                default:
                    frontChick = noEddFrontChick;
                    sideChick = noEggSideChick;
                    backChick = noEggBackChick;

                    animFront = noEggAnimFront;
                    animSide = noEggAnimSide;
                    animBack = noEggAnimBack;

                    break;
            }
        }
        
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        if(yAxis < 0)
        {
            if (frontChick.activeSelf == false)
            {
                frontChick.SetActive(true);
                animFront.SetBool("userInput", true);
            }

            if(pecking)
            {
                animFront.SetBool("pecking", true);
            }
            else
            {
                animFront.SetBool("pecking", false);
            }

            sideChick.SetActive(false);
            backChick.SetActive(false);
        }
        else if(yAxis > 0)
        {
            if (backChick.activeSelf == false)
            {
                backChick.SetActive(true);
                animBack.SetBool("userInput", true);
            }

            if (pecking)
            {
                animBack.SetBool("pecking", true);
            }
            else
            {
                animBack.SetBool("pecking", false);
            }

            frontChick.SetActive(false);
            sideChick.SetActive(false);
        }
        else if(xAxis > 0)
        {
            if(facingRight)
                Flip();

            if (sideChick.activeSelf == false || !animSide.GetBool("userInput"))
            {
                sideChick.SetActive(true);
                animSide.SetBool("userInput", true);
            }

            if (pecking)
            {
                animSide.SetBool("pecking", true);
            }
            else
            {
                animSide.SetBool("pecking", false);
            }

            frontChick.SetActive(false);
            backChick.SetActive(false);
        }
        else if (xAxis < 0)
        {
            if (!facingRight)
                Flip();

            if (sideChick.activeSelf == false || !animSide.GetBool("userInput"))
            {
                sideChick.SetActive(true);
                animSide.SetBool("userInput", true);
            }

            if (pecking)
            {
                animSide.SetBool("pecking", true);
            }
            else
            {
                animSide.SetBool("pecking", false);
            }

            frontChick.SetActive(false);
            backChick.SetActive(false);
        }
        else
        {
            animFront.SetBool("userInput", false);
            animBack.SetBool("userInput", false);
            animSide.SetBool("userInput", false);

            if (PlayerScript.direct == Player.Direction.Down)
            {
                if (pecking)
                {
                    animFront.SetBool("pecking", true);
                }
                else
                {
                    animFront.SetBool("pecking", false);
                }

                if (frontChick.activeSelf == false)
                    frontChick.SetActive(true);

                sideChick.SetActive(false);
                backChick.SetActive(false);
            }
            else if (PlayerScript.direct == Player.Direction.Up)
            {
                if (pecking)
                {
                    animBack.SetBool("pecking", true);
                }
                else
                {
                    animBack.SetBool("pecking", false);
                }

                if (backChick.activeSelf == false)
                    backChick.SetActive(true);

                frontChick.SetActive(false);
                sideChick.SetActive(false);
            }
            else if (PlayerScript.direct == Player.Direction.Right)
            {
                if (facingRight)
                    Flip();

                if (pecking)
                {
                    animSide.SetBool("pecking", true);
                }
                else
                {
                    animSide.SetBool("pecking", false);
                }

                if (sideChick.activeSelf == false)
                    sideChick.SetActive(true);

                frontChick.SetActive(false);
                backChick.SetActive(false);
            }
            else if (PlayerScript.direct == Player.Direction.Left)
            {
                if (!facingRight)
                    Flip();

                if (pecking)
                {
                    animSide.SetBool("pecking", true);
                }
                else
                {
                    animSide.SetBool("pecking", false);
                }

                if (sideChick.activeSelf == false)
                    sideChick.SetActive(true);

                frontChick.SetActive(false);
                backChick.SetActive(false);
            }
        }

        if(PlayerScript.dashing)
        {
            StartCoroutine(DashEffect());
        }
    }

    IEnumerator DashEffect()
    {
        dashEffect.Play();
        animFront.SetBool("userInput", false);
        animBack.SetBool("userInput", false);
        animSide.SetBool("userInput", false);

        if (PlayerScript.direct == Player.Direction.Down)
        {
            if (frontChick.activeSelf == false)
                frontChick.SetActive(true);

            sideChick.SetActive(false);
            backChick.SetActive(false);
        }
        else if (PlayerScript.direct == Player.Direction.Up)
        {
            if (backChick.activeSelf == false)
                backChick.SetActive(true);

            frontChick.SetActive(false);
            sideChick.SetActive(false);
        }
        else if (PlayerScript.direct == Player.Direction.Right)
        {
            if (facingRight)
                Flip();

            if (sideChick.activeSelf == false)
                sideChick.SetActive(true);

            frontChick.SetActive(false);
            backChick.SetActive(false);
        }
        else if (PlayerScript.direct == Player.Direction.Left)
        {
            if (!facingRight)
                Flip();

            if (sideChick.activeSelf == false)
                sideChick.SetActive(true);

            frontChick.SetActive(false);
            backChick.SetActive(false);
        }

        yield return new WaitForSeconds(dashEffectDuration);
        dashEffect.Stop();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
