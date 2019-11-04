using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour
{
    bool ispaused;

    public GameObject menu;

    private void Start()
    {
        ispaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (ispaused)
            {
                
                Time.timeScale = 1;
                ispaused = false;
                menu.SetActive(false);

            }
            else
            {
               
                Time.timeScale = 0;
                ispaused = true;
                menu.SetActive(true);
            }
        }
    }



    public void PauseGame()
    {
        if (ispaused)
        {
            Time.timeScale = 1;
            ispaused = false;
            menu.SetActive(false);

        }
        else
        {
            Time.timeScale = 0;
            ispaused = true;
            menu.SetActive(true);
        }
    }


    public void Playgame()
    {
        Time.timeScale = 1;
        ispaused = false;
        menu.SetActive(false);
    }
   
}
