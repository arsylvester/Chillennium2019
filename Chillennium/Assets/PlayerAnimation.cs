using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public bool userInput;
    public Player PlayerScript;

    private float xAxis;
    private float yAxis;

    [SerializeField] Animator animFront;
    [SerializeField] Animator animSide;
    [SerializeField] Animator animBack;
    [SerializeField] GameObject frontChick;
    [SerializeField] GameObject sideChick;
    [SerializeField] GameObject backChick;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        frontChick.SetActive(false);
        backChick.SetActive(false);
        sideChick.SetActive(true);
        animSide.SetBool("userInput", true);

        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        if(yAxis < 0)
        {
            if (frontChick.activeSelf == false)
            {
                frontChick.SetActive(true);
                animFront.SetBool("userInput", true);
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

            frontChick.SetActive(false);
            sideChick.SetActive(false);
        }
        else if(xAxis != 0)
        {
            if (sideChick.activeSelf == false)
            {
                sideChick.SetActive(true);
                animSide.SetBool("userInput", true);
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
                if (sideChick.activeSelf == false)
                    sideChick.SetActive(true);

                frontChick.SetActive(false);
                backChick.SetActive(false);
            }
        }
    }
}
