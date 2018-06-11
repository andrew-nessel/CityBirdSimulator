using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpMenuUI : MonoBehaviour {

    public Text leftText;
    public Text rightText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateText(string left, string right)
    {
        leftText.text = "Press A: " + left;
        rightText.text = "Press D: " + right;
    }
}
