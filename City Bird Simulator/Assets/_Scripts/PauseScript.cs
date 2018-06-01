using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    public GameObject pauseMenuUI;
    public GameObject gameManager;
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Resume()
    {
        Screen.lockCursor = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameManager.GetComponent<GameManagerBehaviour>().isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Menu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
