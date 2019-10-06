/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrontAnim : MonoBehaviour
{
    public bool userInput;
    public Player PlayerScript;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerScript.yAxis < 0)
        {
            anim.SetBool("userInput", true);
        }
        else
        {
            anim.SetBool("userInput", false);

            if(PlayerScript.direct != Player.Direction.Down)
            {
                gameObject.;
            }
        }
    }
}
*/