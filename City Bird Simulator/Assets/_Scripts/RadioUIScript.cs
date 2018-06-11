using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioUIScript : MonoBehaviour {

    public Text RadioText;
    public Text RadioOverflowText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void firstContact()
    {
        RadioText.text = "  --Come in private, this is your";
        RadioOverflowText.text = "commanding officer";
    }

    public void firstContact2()
    {
        RadioText.text = "Your mission is to bomb as many ";
        RadioOverflowText.text = "civilians as you can";
    }
    public void firstContact3()
    {
        RadioText.text = "Remember to steal any food you can,";
        RadioOverflowText.text = "it should improve your chances";
    }

    public void firstContact4()
    {
        RadioText.text = "Good luck, private";
        RadioOverflowText.text = "";
    }

    public void extraction()
    {
        RadioText.text = "Execellent work private!";
        RadioOverflowText.text = "";
    }

    public void extraction1()
    {
        RadioText.text = "Extraction is now available";
        RadioOverflowText.text = "(check in high places)";
    }

    public void extraction2()
    {
        RadioText.text = "Of course you can keep fighting";
        RadioOverflowText.text = "but don't overstay your welcome";
    }
}
