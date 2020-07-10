using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public bool canInteract = false;
    private PlayerAnimation playAn;
    [SerializeField] GameObject dNotEnough;
    [SerializeField] GameObject Enough;
    [SerializeField] UIController ui;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && Input.GetButtonDown("Interact"))
        {
            print("interacted");
            if(playAn.numEggs >= 4)
            {
                Application.LoadLevel("LevelTwo");
            }
            else
            {
                ui.openDialog(dNotEnough);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player)
        {
            playAn = player.GetComponent<PlayerAnimation>();
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            canInteract = false;
        }
    }
}
