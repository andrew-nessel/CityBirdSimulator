using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermalBehaviour : MonoBehaviour
{

    public float liftAmount;
    public float cooldownInSeconds;
    public ParticleSystem paSystem;

    private bool onCooldown;
    private float timeTillReactive;

    // Use this for initialization
    void Start()
    {
        onCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onCooldown)
        {
            if (timeTillReactive <= 0f)
            {
                onCooldown = false;
                ParticleSystem.MainModule ma = paSystem.main;
                ma.startColor = Color.white;
            }
            else
            {
                timeTillReactive -= Time.deltaTime;
            }
        }
    }

    public void goIntoCooldown()
    {
        ParticleSystem.MainModule ma = paSystem.main;
        ma.startColor = new Color(1, 0, 0, 1);
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
