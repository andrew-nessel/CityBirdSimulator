﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehaviour : MonoBehaviour {

    public int Bombtype;
    public int NumberOfBombs;
    public float extraTurning;
    public float extraSpeed;
    public float extraThermalLift;

    public string FlightAsideText;
    public string BombDsideText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(new Vector3(0f, 75f, 0f) * Time.deltaTime);
	}
}
