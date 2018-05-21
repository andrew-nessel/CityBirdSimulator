using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    public GameObject pauseMenuUI;
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Resume()
    {
        Screen.lockCursor = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Menu()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

}
