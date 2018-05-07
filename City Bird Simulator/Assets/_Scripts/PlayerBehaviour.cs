using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public float speed;
    public float tilt;

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tilt = 0.5f;
        speed = 0.0f;
    }
	
	void FixedUpdate () {


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        tilt = tilt - vertical;
        if (tilt > 1)
            tilt = 1f;
        if (tilt < 0)
            tilt = 0f;
        
        float lift = tilt * 6f + 6.8f;
        float fallSpeed = 9.8f - lift;

        float momentum = fallSpeed * .1f;

        //if (fallSpeed < 0.0f)
        //{
        //    momentum = 0.0f - momentum;
        //}

        speed = speed + momentum;

        if (speed < -0.01f)
        {
            speed = 0.0f;
            lift = 9.8f;
        }

        Debug.Log(speed);

        Vector3 liftVector = new Vector3(0.0f, lift, 0.0f);
        rb.AddForce(liftVector);       

	}

}
