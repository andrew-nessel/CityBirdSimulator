﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

    public int predictionStepsPerFrame = 12;

    public Vector3 bombVelocity;
    public float speed; 
    float stepSize = 0.01f;
    public Rigidbody r;

    // Use this for initialization
    void Start () {
        r = GetComponent<Rigidbody>();
        speed = GetComponent<PlayerBehaviour>().speed;
        r.velocity = GetComponent<PlayerBehaviour>().rb.velocity;
        float velocity = ((Mathf.Abs(speed) + 1) * 2);
        bombVelocity = GetComponent<PlayerBehaviour>().transform.forward * velocity;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Start values
        Vector3 currentVelocity = this.transform.forward * speed;
        Vector3 currentPosition = this.transform.position;

        Vector3 newPosition = Vector3.zero;
        Vector3 newVelocity = Vector3.zero;
        //for (float index = 0; index < Time.fixedDeltaTime; index += (Time.fixedDeltaTime / 6))
        //{
            //Calculate the new position of the bullet
            CurrentIntegrationMethod((Time.fixedDeltaTime), currentPosition, currentVelocity, out newPosition, out newVelocity);

            currentPosition = newPosition;
            currentVelocity = newVelocity;
      // }
        this.transform.position = currentPosition;
        //r.velocity = currentVelocity;
        /*
        Vector3 point1 = this.transform.position;
        float stepSize = 1.0f / predictionStepsPerFrame;
        for (float step = 0; step < 1; step += stepSize)
        {
            bombVelocity += Physics.gravity * stepSize * Time.deltaTime;
            Vector3 point2 = point1 + bombVelocity * stepSize * Time.deltaTime;

            Ray ray = new Ray(point1, point2 - point1);
            if (Physics.Raycast(ray, (point2 - point1).magnitude))
            {
                //Some poor target has been hit
                Debug.Log("Hit");
            }
            point1 = point2;
        }
        this.transform.position = point1;*/
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Debug.Log("Bomb hit a TARGET");
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Bomb")
        {
            //Do nothing
        }
        else
        {
            Debug.Log("Bomb hit something else");
            Destroy(gameObject);
        }

    }
    //Easier to change integration method once in this method
    public static void CurrentIntegrationMethod(
        float h,
        Vector3 currentPosition,
        Vector3 currentVelocity,
        out Vector3 newPosition,
        out Vector3 newVelocity)
    {
        //IntegrationMethods.EulerForward(h, currentPosition, currentVelocity, out newPosition, out newVelocity);
        IntegrationMethods.Heuns(h, currentPosition, currentVelocity, out newPosition, out newVelocity);
        //IntegrationMethods.RungeKutta(h, currentPosition, currentVelocity, out newPosition, out newVelocity);
        //IntegrationMethods.BackwardEuler(h, currentPosition, currentVelocity, out newPosition, out newVelocity);
    }

}
