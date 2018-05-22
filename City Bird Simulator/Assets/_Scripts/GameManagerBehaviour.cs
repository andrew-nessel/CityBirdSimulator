using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour {

    public Vector3 spawnLocation;
    public GameObject player;
    public GameObject camera;
    public GameObject PauseMenu;
    public GameObject EndMenu;
    public GameObject HUDUI;
    public bool isPaused = false;
    public bool isHUD = false;
    public bool bombActive = false;

    public int Targets;
    public int Bombs;

    // Use this for initialization
    void Start () {
        Targets = 0;
        UpdateBombs(Bombs);
        UpdateTargets(Targets);
        Screen.lockCursor = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (player.GetComponent<PlayerBehaviour>().winCondition)
        {
            isPaused = true;
            Screen.lockCursor = false;
            EndMenu.GetComponent<EndMenuScript>().Victory();

        }
        else if (player.GetComponent<PlayerBehaviour>().collide)
        {
            isPaused = true;
            Screen.lockCursor = false;
            EndMenu.GetComponent<EndMenuScript>().Defeat();
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (isPaused)
                {
                    isPaused = false;
                    Screen.lockCursor = true;
                    PauseMenu.GetComponent<PauseScript>().Resume();
                }
                else
                {
                    isPaused = true;
                    Screen.lockCursor = false;
                    PauseMenu.GetComponent<PauseScript>().Pause();
                }
            }

            if (!isPaused)
            {
                if (Input.GetKeyUp(KeyCode.Tab))
                {
                    if (isHUD)
                    {
                        HUDUI.SetActive(false);
                        isHUD = false;
                    }
                    else
                    {
                        HUDUI.SetActive(true);
                        isHUD = true;
                    }
                }
            }
        }


    }

    public void UpdateBombs(int bombs)
    {
        HUDUI.GetComponent<HUDUIScript>().UpdateBombCounter(bombs);
        Bombs = bombs;
    }
    

    public void UpdateTargets(int targets)
    {
        HUDUI.GetComponent<HUDUIScript>().UpdateTargetCounter(targets);
        Targets = targets;
    }

    public bool CanDropBomb()
    {
        return ((!bombActive) && (Bombs > 0));
    }

}
