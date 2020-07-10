using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    public Canvas menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void credits(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void quit()
    {
        Application.Quit();
    }
}
