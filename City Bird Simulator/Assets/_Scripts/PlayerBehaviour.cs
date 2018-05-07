using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public float speed;
    public float tilt;
    public float Rtilt;

    public Rigidbody rb;
    private Vector3 previousVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tilt = 0.5f;
        Rtilt = 0.0f;
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

        Rtilt = Rtilt - horizontal;
        if (Rtilt > 1)
            Rtilt = 1f;
        if (Rtilt < -1)
            Rtilt = -1f;

        float lift = tilt * 6f + 6.8f;
        float fallSpeed = 9.8f - lift;

        float momentum = fallSpeed * .1f;

        speed = speed + momentum;

        if (speed < 0.01f)
        {
            speed = 0.0f;
            lift = 9.8f;
            rb.velocity = new Vector3 (0, 0, 0);
        }else if(speed > 30.0f)
        {
            speed = 10.0f;
        }

        Debug.Log((speed) + " " + (Rtilt * speed));
        

        Vector3 liftVector = new Vector3(Rtilt * speed, lift, speed);
        rb.AddRelativeForce(liftVector); 

        //rb.AddRelativeForce(Vector3.forward * speed);

        previousVelocity = rb.velocity;

	}

}
