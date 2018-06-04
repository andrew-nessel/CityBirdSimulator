using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUIScript : MonoBehaviour {

    
    public Text BombCounterText;
    public Text TargetCounterText;
    public Text TimerText;
    public GameObject standardBombImage;
    public GameObject bigBombImage;
    public GameObject BombCameraPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateBombCounter(int num, int type)
    {
        BombCounterText.text = num.ToString();

        if(type == 1)
        {
            standardBombImage.SetActive(false);
            bigBombImage.SetActive(true);
        }
        else
        {
            standardBombImage.SetActive(true);
            bigBombImage.SetActive(false);
        }
    }

    public void UpdateTargetCounter(int num)
    {
        TargetCounterText.text = num.ToString();
    }

    public void UpdateTimer(float seconds)
    {
        //TimerText.text = Mathf.Floor(seconds/60) + ":" + Mathf.Floor(seconds%60);

        int minutesLeft = Mathf.FloorToInt(seconds / 60);
        int secondsLeft = Mathf.FloorToInt(seconds % 60);

        string secondHand = secondsLeft.ToString();
        if (secondsLeft < 10)
        {
            secondHand = "0" + secondsLeft.ToString();
        }

        TimerText.text = minutesLeft + ":" + secondHand;
    }

    public void turnOffBombCamera()
    {
        BombCameraPanel.SetActive(false);
    }
    public void turnOnBombCamera()
    {
        BombCameraPanel.SetActive(true);
    }
}
