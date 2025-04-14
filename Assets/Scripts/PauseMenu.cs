using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        PauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
