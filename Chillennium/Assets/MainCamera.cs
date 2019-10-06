using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Player player;
    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        test.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        if(playerScreenPosition.y > Screen.height)
        {
            print("Above camera");
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * 1.5f, 0));
        }
        else if(playerScreenPosition.y < 0)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, -Screen.height * 1.5f, 0));
        }
        else if (playerScreenPosition.x < 0)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.width * 1.5f, Screen.height / 2, 0));
        }
        else if (playerScreenPosition.x > Screen.width)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 1.5f, Screen.height * 1.5f, 0));
        }
    }
}
