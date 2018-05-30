using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUIScript : MonoBehaviour {

    
    public Text BombCounterText;
    public Text TargetCounterText;
    public GameObject BombCameraPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateBombCounter(int num)
    {
        BombCounterText.text = num.ToString();
    }

    public void UpdateTargetCounter(int num)
    {
        TargetCounterText.text = num.ToString();
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
