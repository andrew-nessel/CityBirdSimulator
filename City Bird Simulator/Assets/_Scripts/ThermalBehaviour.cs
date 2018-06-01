using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermalBehaviour : MonoBehaviour {

    public float liftAmount;
    public float cooldownInSeconds;

    private bool onCooldown;
    private float timeTillReactive;

	// Use this for initialization
	void Start () {
        onCooldown = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (onCooldown)
        {
            if(timeTillReactive <= 0f)
            {
                onCooldown = false;
            }
            else
            {
                timeTillReactive -= Time.deltaTime;
                Debug.Log(timeTillReactive);
            }
        }
	}

    public void goIntoCooldown()
    {
        onCooldown = true;
        timeTillReactive = cooldownInSeconds;
    }

    public bool isOnCooldown()
    {
        return onCooldown;
    }

    public float getLift()
    {
        if (onCooldown)
        {
            return 0f;
        }
        else
        {
            return liftAmount;
        }
    }
}
