using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //PAUSE
    public GameObject pausePanel;
    public GameObject deathPanel;
    public bool notpause = true;
    public bool pausekey;


    void Start()
    {
        notpause = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetPause(); 
    }

    void SetPause() {

        if(Input.GetKeyDown(KeyCode.P))
        {
            pausekey = !pausekey;

            if(pausekey && !notpause)
            {
                //AudioController.audioSource.Pause();
                PauseOn();
                Time.timeScale = 0;
            }
            else
            {
                PauseOff();
                Time.timeScale = 1;
                //AudioController.audioSource.Play();

            }
        }
    }

    void PauseOn()
    {
        pausePanel.SetActive(true);
    }

    public void PauseOff()
    {
        pausekey = false;
        pausePanel.SetActive(false);
    }

    public void QuitButton() {
        Application.Quit();
    }

    public void DeathScreen() {
        deathPanel.SetActive(true);
    }
}
