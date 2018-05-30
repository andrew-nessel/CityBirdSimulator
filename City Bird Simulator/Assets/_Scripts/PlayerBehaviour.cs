using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    public float tilt;
    public float Rtilt;
    public bool collide = false;
    public bool winCondition = false;
    public float maxspeed;
    public GameObject Bomb;
    public GameObject GameManager;

    public Rigidbody rb;
    private Vector3 previousVelocity;
    private bool inThermal;

    public LineRenderer lineRenderer;

    //Sound Variables
    public AudioClip pigeonCoo;
    public AudioClip Squawk;
    public AudioClip pigeon;
    public AudioClip caw;

    public Random rand;
    private AudioSource source;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    void Start()
    {
        rand = new Random();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        tilt = 0.5f;
        Rtilt = 0.0f;
        speed = 0.0f;
        maxspeed = 20.0f;
        inThermal = false;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.useWorldSpace = true;
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.GetComponent<GameManagerBehaviour>().isPaused)
        {
            if(GameManager.GetComponent<GameManagerBehaviour>().CanDropBomb())
            {
                if (Input.GetMouseButton(0))
                {
                    lineRenderer.useWorldSpace = true;
                    DrawTrajectoryPath();
                    //BuildTrajectoryLine(BuildTrajPath()); 
                }
                if (Input.GetMouseButtonUp(0))
                {
                    //source.pitch = Random.Range(lowPitchRange, highPitchRange);
                    GameObject go = Instantiate(Bomb, new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z), transform.rotation);
                    // give it the same velocity as the current object
                    //go.GetComponent<Rigidbody>().velocity = rb.velocity;
                    go.GetComponent<BombBehaviour>().speed = speed;
                    go.GetComponent<BombBehaviour>().GameManager = GameManager;
                    lineRenderer.useWorldSpace = false;

                    GameManager.GetComponent<GameManagerBehaviour>().UpdateBombs(GameManager.GetComponent<GameManagerBehaviour>().Bombs - 1);
                    GameManager.GetComponent<GameManagerBehaviour>().activateBomb();
                    GameObject bombCam = GameManager.GetComponent<GameManagerBehaviour>().BombCamera;
                    bombCam.transform.rotation = rb.rotation;
                    bombCam.transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
                    go.GetComponent<BombBehaviour>().BombCamera = bombCam;
                }
            }
            int playSound = Random.Range(0, 2000);
            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            float vol = Random.Range(volLowRange, volHighRange);
            if ( playSound < 1 && speed < 5f && !source.isPlaying)
            {
                source.PlayOneShot(pigeonCoo, vol);
            }
            else if(playSound < 3 && speed < 5f &&!source.isPlaying)
            {
                source.PlayOneShot(pigeon, vol);
            }
            
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

        if (inThermal)
        {
            lift = lift + 10f;
        }

        float birdTilt = 0f;

        if (tilt < 0.5f)
        {
            birdTilt = (45f * (.5f - tilt));
        }
        else
        {
            birdTilt = ((tilt - .5f) * -45f);
        }

        transform.eulerAngles = new Vector3(birdTilt, Rtilt, 0f);
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

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bomb")
        {
            //Do nothing
        }
        else if (collision.gameObject.tag == "Goal")
        {
            //Debug.Log("a problem");
            if(collide == false)
            {
                collide = true;
                winCondition = true;
                Debug.Log("WE WON");
            }
        }
        else
        {
            collide = true;
            Debug.Log("WE COLLIDED");
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Thermal")
        {
            Debug.Log("Player hit a Thermal");
            inThermal = true;
        }
        else
        {
            //Do nothing
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Thermal")
        {
            Debug.Log("Player exited a Thermal");
            inThermal = false;
        }
        else
        {
            //Do nothing
        }
    }

    void BuildTrajectoryLine(List<Vector3> positions)
    {
        lineRenderer.SetVertexCount(positions.Count);
        for (var i = 0; i < positions.Count; ++i)
        {
            lineRenderer.SetPosition(i, positions[i]);
        }
    }
    List<Vector3> BuildTrajPath()
    {
        var positions = new List<Vector3>();
        Vector3 start = this.transform.position;
        Vector3 initialVelocity = rb.GetPointVelocity(start);
        Vector3 predictedPoint = start;
        for(float i = 0; i < 1; i+= 0.1f)
        {
            positions.Add(predictedPoint);
            predictedPoint = (start + initialVelocity) + (Physics.gravity * .5f);
        }

        /*float stepSize = 1.0f / 12;
        Vector3 point1 = this.transform.position;
        Vector3 bombVelocity = rb.velocity;
        Vector3 predictedBombVelocity = this.transform.forward;
        for (float step = 0; step < 1; step += stepSize)
        {
            positions.Add(point1);
            predictedBombVelocity = Physics.gravity * step * step * .5f;
            Vector3 point2 = point1 + (bombVelocity * step) + predictedBombVelocity;
            point1 = point2;
        }*/
        return positions;
    }
    //Display the trajectory path with a line renderer
    public void DrawTrajectoryPath()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        //How long did it take to hit the target?
        float timeToHitTarget = 20.0f;//CalculateTimeToHitTarget();

        //How many segments we will have
        int maxIndex = Mathf.RoundToInt(timeToHitTarget) * 4;

        //Start values
        Vector3 currentVelocity = this.transform.forward * speed;
        Vector3 currentPosition = this.transform.position;
        currentPosition.y -= 1;

        Vector3 newPosition = Vector3.zero;
        Vector3 newVelocity = Vector3.zero;

        //Build the trajectory line
        for (int index = 0; index < maxIndex; index++)
        {
            lineRenderer.positionCount = maxIndex;
            lineRenderer.SetPosition(index, currentPosition);

            //Calculate the new position of the bullet
            CurrentIntegrationMethod((Time.fixedDeltaTime) , currentPosition, currentVelocity, out newPosition, out newVelocity);

            currentPosition = newPosition;
            currentVelocity = newVelocity;
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