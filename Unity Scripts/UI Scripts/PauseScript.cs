using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 *  HIDES/SHOWS ALL CHILD OBJECTS AND PAUSES/UNPAUSES ON ESC
 *  APPLY TO PARENT OBJECT WITH PAUSE CANVAS AS ITS CHILDREN
 * 
 */
public class PauseScript : MonoBehaviour
{
    private bool gamePaused = false;
    void Start()
    {
        closePause();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                openPause();
                gamePaused = true;
            }
            else if (gamePaused)
            {
                closePause();
                gamePaused = false;
            }
        }
    }

    private void closePause()
    {
        Time.timeScale = 1;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void openPause()
    {
        Time.timeScale = 0;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}