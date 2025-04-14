using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseScreen;
    public bool isPaused;
    public GameObject HowToPlay;
    public GameObject ComboList;
    public GameObject PauseShader;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Pressed");
            if (isPaused == false)
            {
                Pause();
                Debug.Log("Paused");
            }
            else
            {
                Continue();
                Debug.Log("Not Paused");
            }
        }

    }

   /*public void OpenPauseShader()
    {
        PauseShader.SetActive(true);
    }*/
    
    public void Pause()
    {
        HowToPlay.SetActive(false);
        ComboList.SetActive(false);
        PauseShader.SetActive(true);
        PauseScreen.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PauseScreen.SetActive(false);
        PauseShader.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenHowToPlay()
    {
        PauseScreen.SetActive(false);
        HowToPlay.SetActive(true);
    }

    public void OpenComboList()
    {
        PauseScreen.SetActive(false);
        ComboList.SetActive(true);
    }
}
