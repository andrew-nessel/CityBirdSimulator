using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float speed;
    public float tilt;
    public float Rtilt;
    public bool collide = false;
    public float maxspeed;
    public GameObject Bomb;

    public Rigidbody rb;
    private Vector3 previousVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        tilt = 0.5f;
        Rtilt = 0.0f;
        speed = 0.0f;
        maxspeed = 20.0f;
    }

    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            GameObject go = Instantiate(Bomb, new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z), transform.rotation);
            go.GetComponent<Rigidbody>().velocity = new Vector3(rb.velocity.x, rb.velocity.y - 2f, rb.velocity.z);
        }
    }

    private void FixedUpdate()
    {
        if (collide)
        {
            rb.useGravity = true;
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        tilt = tilt - (vertical / 10);
        if (tilt > 1)
            tilt = 1f;
        if (tilt < 0)
            tilt = 0f;

        Rtilt = Rtilt + horizontal;
        if (Rtilt > 360)
            Rtilt = Rtilt - 360f;
        if (Rtilt < -1)
            Rtilt = 360f + Rtilt;

        float lift = (tilt * 12f) - 6.5f;
        float fallSpeed = -lift;// - 9.8f;

        float momentum = fallSpeed * .05f;



        speed = speed + momentum;

        if (speed < 0.0f)
        {
            speed = 0.1f;
            lift = -0.5f;
        }
        else if (speed > maxspeed)
        {
            speed = maxspeed;
        }

        float velocity = (Mathf.Abs(speed) + 1) * 2;


        transform.eulerAngles = new Vector3(0, Rtilt, 0);
        Vector3 velocityV = new Vector3();

        float f1 = Rtilt;

        if (Rtilt < 90)
        {
            //+x
            //+z
            velocityV.x = (Rtilt / 90) * velocity;
            velocityV.z = (1 - (Rtilt / 90)) * velocity;
        }
        else if (Rtilt < 180)
        {
            //+x
            //-z
            f1 = f1 - 90;
            velocityV.x = (1 - (f1 / 90)) * velocity;
            velocityV.z = 0 - ((f1 / 90) * velocity);
        }
        else if (Rtilt < 270)
        {
            //-x
            //-z
            f1 = f1 - 180;
            velocityV.x = 0 - ((f1 / 90) * velocity);
            velocityV.z = 0 - ((1 - (f1 / 90)) * velocity);
        }
        else
        {
            //-x
            //+z
            f1 = f1 - 270;
            velocityV.x = 0 - ((1 - (f1 / 90)) * velocity);
            velocityV.z = (f1 / 90) * velocity;
        }


        //Debug.Log((speed) + " " + (fallSpeed));

        velocityV.y = lift;
        //velocityV.y = rb.velocity.y + lift;

        previousVelocity = rb.velocity;

        rb.velocity = (velocityV);
    }

    void nFixedUpdate() //This is an old implementation that used True Physics
    {

        if (collide)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        tilt = tilt - vertical;
        if (tilt > 1)
            tilt = 1f;
        if (tilt < 0)
            tilt = 0f;

        Rtilt = Rtilt + horizontal;
        if (Rtilt > 360)
            Rtilt = Rtilt - 360f;
        if (Rtilt < -1)
            Rtilt = 360f + Rtilt;

        float lift = tilt * 12f + 4f;
        float fallSpeed = 9.8f - lift;

        float momentum = fallSpeed * .05f;

        //if(momentum < 0.01f)
        //{
        //  momentum = momentum;
        //}

        speed = speed + momentum;

        if (speed < (-maxspeed))
        {
            speed = (-maxspeed);
            lift = 9.0f;
            //rb.velocity = new Vector3(0, -0.5f, 0);
        }
        else if (speed > maxspeed)
        {
            speed = maxspeed;
        }


        transform.eulerAngles = new Vector3(0, Rtilt, 0);
        Vector3 velocityV = new Vector3();

        float f1 = Rtilt;

        if ((speed < maxspeed) && (speed > (-maxspeed)))
        {
            if (Rtilt < 90)
            {
                //+x
                //+z
                velocityV.x = (Rtilt / 90) * speed * 2;
                velocityV.z = (1 - (Rtilt / 90)) * speed * 2;


                velocityV.y = 0;
            }
            else if (Rtilt < 180)
            {
                //+x
                //-z
                f1 = f1 - 90;
                velocityV.x = (f1 / 90) * speed * 2;
                velocityV.z = 0 - ((1 - (f1 / 90)) * speed * 2);
                velocityV.y = 0;
            }
            else if (Rtilt < 270)
            {
                //-x
                //-z
                f1 = f1 - 180;
                velocityV.x = 0 - ((f1 / 90) * speed * 2);
                velocityV.z = 0 - ((1 - (f1 / 90)) * speed * 2);
                velocityV.y = 0;
            }
            else
            {
                //-x
                //+z
                f1 = f1 - 270;
                velocityV.x = 0 - ((f1 / 90) * speed * 2);
                velocityV.z = (1 - (f1 / 90)) * speed * 2;
                velocityV.y = 0;
            }
        }

        //Debug.Log((speed) + " " + (momentum));


        Vector3 liftVector = new Vector3(0, lift, 0);
        rb.AddForce(liftVector);

        rb.AddRelativeForce(velocityV);

        previousVelocity = rb.velocity;

    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bomb")
        {
            //Do nothing
        }
        else
        {
            collide = true;
            Debug.Log("WE COLLIDED");
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Player hit a Thermal");
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + 10f, rb.velocity.z);
    }
}
