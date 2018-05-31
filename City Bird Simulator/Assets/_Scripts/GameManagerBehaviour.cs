using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour {
	protected static GameManagerBehaviour _instance = null;
	
	public static GameManagerBehaviour Instance {
		get {
			if (_instance == null){
				_instance = FindObjectOfType<GameManagerBehaviour>();
				if (_instance == null) {
					Debug.LogError("An instance of GameManager is required but does not exist");
				}
			}
			return _instance;
		}
	}
	
    public Vector3 spawnLocation;
    public GameObject player;
    public GameObject camera;
    public GameObject PauseMenu;
    public GameObject EndMenu;
    public GameObject HUDUI;
    public GameObject BombCamera;
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

    public void deactivateBomb()
    {
        bombActive = false;
        //BombCamera.SetActive(false);
        HUDUI.GetComponent<HUDUIScript>().turnOffBombCamera();
    }

    public void activateBomb()
    {
        bombActive = true;
        BombCamera.SetActive(true);
        HUDUI.GetComponent<HUDUIScript>().turnOnBombCamera();
    }
	
	//Below = PowerUp activation
	public void activatePowerUp(){
		Bombs += 1;
		UpdateBombs(Bombs);
	}
}
