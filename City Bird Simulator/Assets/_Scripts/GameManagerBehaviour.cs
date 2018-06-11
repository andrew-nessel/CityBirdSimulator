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
    public GameObject Goal;
    public GameObject currentPowerUp;
    public GameObject PowerUpUI;

    public bool isPaused = false;
    public bool isHUD = false;
    public bool bombActive = false;
    public bool isPowerUpWait = false;

    public int Targets;
    public int Bombs;
    public int timeInSeconds;
    public int targetsToGoal;
    public int bombType;

    public int powerUpBombNumber;
    private float timerAmount;

    // Use this for initialization
    void Start () {
        Targets = 0;
        bombType = 0;
        UpdateBombs(Bombs, 0);
        UpdateTargets(Targets);
        timerAmount = 0;
        Goal.SetActive(false);
        Screen.lockCursor = true;
        Time.timeScale = 1f;
    }
	
	// Update is called once per frame
	void Update () {

        if (player.GetComponent<PlayerBehaviour>().winCondition)
        {
            isPaused = true;
            Screen.lockCursor = false;
            EndMenu.GetComponent<EndMenuScript>().Victory();
            int timeScore = Mathf.FloorToInt(timeInSeconds - timerAmount);
            if(timeScore < 0)
            {
                timeScore = 0;
            }
            EndMenu.GetComponent<EndMenuScript>().UpdateScore(timeScore + (Targets*10));
            HUDUI.SetActive(false);
        }
        else if (player.GetComponent<PlayerBehaviour>().collide)
        {
            isPaused = true;
            Screen.lockCursor = false;
            Time.timeScale = .333f;
            EndMenu.GetComponent<EndMenuScript>().Defeat();
            HUDUI.SetActive(false);
        }
        else
        {

            if (isPowerUpWait)
            {

                Debug.Log(currentPowerUp.GetComponent<PowerupBehaviour>().FlightAsideText);

                PowerUpUI.GetComponent<PowerUpMenuUI>().updateText(currentPowerUp.GetComponent<PowerupBehaviour>().FlightAsideText, currentPowerUp.GetComponent<PowerupBehaviour>().BombDsideText);
                PowerUpUI.SetActive(true);
                
                if (Input.GetKeyUp("a"))
                {

                    float extraSpeed = currentPowerUp.GetComponent<PowerupBehaviour>().extraSpeed;
                    float extraTurning = currentPowerUp.GetComponent<PowerupBehaviour>().extraTurning;
                    float extraThermalLift = currentPowerUp.GetComponent<PowerupBehaviour>().extraThermalLift;

                    Time.timeScale = 1f;
                    isPowerUpWait = false;

                    player.GetComponent<PlayerBehaviour>().extraTurning += extraTurning;
                    player.GetComponent<PlayerBehaviour>().extraThermalLift += extraThermalLift;
                    player.GetComponent<PlayerBehaviour>().maxspeed += extraSpeed;
                    player.GetComponent<PlayerBehaviour>().diveMaxSpeed += extraSpeed;

                    PowerUpUI.SetActive(false);
                    Destroy(currentPowerUp);
                }
                else if(Input.GetKeyUp("d"))
                {
                    int bombs = currentPowerUp.GetComponent<PowerupBehaviour>().NumberOfBombs;
                    int type = currentPowerUp.GetComponent<PowerupBehaviour>().Bombtype;

                    Time.timeScale = 1f;
                    isPowerUpWait = false;

                    if (type == 0)
                    {
                        UpdateBombs(Bombs + bombs, type);
                    }
                    else
                    {
                        UpdateBombs(bombs, type);
                    }

                    PowerUpUI.SetActive(false);
                    Destroy(currentPowerUp);
                }
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
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

                timerAmount += Time.deltaTime;

                HUDUI.GetComponent<HUDUIScript>().UpdateTimer(timerAmount);
            }
        }


    }

    public void UpdateBombs(int bombs, int type)
    {

        if(type == 0)
        {
            Bombs = bombs;
        }
        else
        {
            if(bombs == 0)
            {
                powerUpBombNumber = 0;
                bombType = 0;
                type = 0;
                bombs = Bombs;

            }
            else
            {
                powerUpBombNumber = bombs;
                bombType = type;
            }
        }

        HUDUI.GetComponent<HUDUIScript>().UpdateBombCounter(bombs, type);

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

    public void activateBombView()
    {
        BombCamera.SetActive(true);
        HUDUI.GetComponent<HUDUIScript>().turnOnBombingVeiw();
    }

    public void deactivateBombView()
    {
        BombCamera.SetActive(false);
        HUDUI.GetComponent<HUDUIScript>().turnOffBombingVeiw();
    }

    public void checkForGoal()
    {
        if (Targets>=targetsToGoal) {
            Goal.SetActive(true);
        }
    }
	
	//Below = PowerUp activation
	public void activatePowerUp(GameObject powerUp)
    {
        currentPowerUp = powerUp;
        Time.timeScale = 0f;
        isPowerUpWait = true;
    }
    
}
