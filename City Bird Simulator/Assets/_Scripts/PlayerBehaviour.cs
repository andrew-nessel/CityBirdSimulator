using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    public float tilt;
    public float Rtilt;
    public bool collide = false;
    public bool winCondition = false;
    public float maxspeed;
    public float diveMaxSpeed;
    public GameObject Bomb;
    public GameObject BigBomb;
    public GameObject GameManager;

    public float extraTurning;
    public float extraThermalLift;

    public Rigidbody rb;
    private Vector3 previousVelocity;
    private float thermalLift;
	
	//Meter Text
	public Text Speedometer;
	public Text Altimeter;
    public GameObject SpeedArrow;
    public GameObject AltArrow;
    public float SpeedValue;
	public float HeightValue;
	Vector3 height;

    public LineRenderer lineRenderer;

    //Sound Variables
    public AudioClip pigeonCoo;
    public AudioClip Squawk;
    public AudioClip pigeon;
    public AudioClip caw;
    public AudioClip missionFail;

    //BirdCollisionSounds
    public AudioClip impact;
    public AudioClip splat;
    public AudioClip rattle;
    public AudioClip metal1;
    public AudioClip metal2;
    public AudioClip thud;
    private bool BirdDeath = true;

    public Random rand;
    private AudioSource source;
    private float lowPitchRange = .9F;
    private float highPitchRange = 1.25F;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    private bool wasDive;
    private GameObject BombCam;
    private GameObject EndMenu;
    void Start()
    {
        rand = new Random();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        tilt = 0.5f;
        Rtilt = 0.0f;
        speed = 0.0f;
        wasDive = false;
        thermalLift = 0f;

        extraTurning = 1;
        extraThermalLift = 1;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.useWorldSpace = true;
        source = GetComponent<AudioSource>();
        BombCam = GameManager.GetComponent<GameManagerBehaviour>().BombCamera;
        EndMenu = GameObject.Find("EndMenu");
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
                    //GameManager.GetComponent<GameManagerBehaviour>().activateBombView();
                    //BombCam.transform.rotation = rb.rotation;
                    BombCam.transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, 0f);

                    Vector3 bombViewpos = new Vector3(0f, 0f, 0f);
                    bombViewpos.y = transform.position.y + 25f;
                    bombViewpos.x = transform.position.x;
                    bombViewpos.z = transform.position.z;

                    if (transform.eulerAngles.y < 90)
                    {
                        //+x
                        //+z
                        bombViewpos.x += 10f * (transform.eulerAngles.y / 90);
                        bombViewpos.z += 10f * (1 - (transform.eulerAngles.y / 90));
                    }
                    else if (transform.eulerAngles.y < 180)
                    {
                        //+x
                        //-z
                        float f1 = transform.eulerAngles.y - 90;
                        bombViewpos.x += 10f * (1 - (f1 / 90));
                        bombViewpos.z += 10f * (0 - (f1 / 90));
                    }
                    else if (transform.eulerAngles.y < 270)
                    {
                        //-x
                        //-z
                        float f1 = transform.eulerAngles.y - 180;
                        bombViewpos.x += 10f * (0 - (f1 / 90));
                        bombViewpos.z += 10f * (0 - (1 - (f1 / 90)));
                    }
                    else
                    {
                        //-x
                        //+z
                        float f1 = transform.eulerAngles.y - 270;
                        bombViewpos.x += 10f * (0 - (1 - (f1 / 90)));
                        bombViewpos.z += 10f * (f1 / 90);
                    }

                    BombCam.transform.position = bombViewpos;
                    //BuildTrajectoryLine(BuildTrajPath()); 
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (Input.GetMouseButton(1))
                    {
                        lineRenderer.useWorldSpace = false;
                        GameManager.GetComponent<GameManagerBehaviour>().deactivateBombView();
                    }
                    else
                    {
                        //source.pitch = Random.Range(lowPitchRange, highPitchRange);
                        GameObject go;
                        if(GameManager.GetComponent<GameManagerBehaviour>().bombType == 1)
                        {
                            go = Instantiate(BigBomb, new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z), transform.rotation);
                            GameManager.GetComponent<GameManagerBehaviour>().UpdateBombs(GameManager.GetComponent<GameManagerBehaviour>().powerUpBombNumber - 1, GameManager.GetComponent<GameManagerBehaviour>().bombType);
                        }
                        else
                        {
                            go = Instantiate(Bomb, new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z), transform.rotation);
                            GameManager.GetComponent<GameManagerBehaviour>().UpdateBombs(GameManager.GetComponent<GameManagerBehaviour>().Bombs - 1, GameManager.GetComponent<GameManagerBehaviour>().bombType);
                        }
                        // give it the same velocity as the current object
                        //go.GetComponent<Rigidbody>().velocity = rb.velocity;
                        go.GetComponent<BombBehaviour>().speed = speed;
                        go.GetComponent<BombBehaviour>().GameManager = GameManager;
                        lineRenderer.useWorldSpace = false;

                        
                        GameManager.GetComponent<GameManagerBehaviour>().activateBomb();
                        BombCam.transform.rotation = rb.rotation;
                        BombCam.transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
                        go.GetComponent<BombBehaviour>().BombCamera = BombCam;

                    }
                }
            }
            
            
        }
		ChangeMeters();

    }
	
	void ChangeMeters(){
		height = new Vector3(0.0f, this.gameObject.transform.position.y, 0.0f);
		HeightValue = height.y;
		
		Speedometer.text = speed.ToString("F2") + "m/s";
        SpeedArrow.transform.eulerAngles = new Vector3(0f, 0f, (float) (85f * (1.05 - (speed/(maxspeed * 1.5)))));
		Altimeter.text = HeightValue.ToString("F2") + "m";

        float heightr = (float)(-80f * (1 - (HeightValue / (120))));

        if(heightr < -90)
        {
            heightr = -90;
        }else if(heightr > -5)
        {
            heightr = -5;
        }

        AltArrow.transform.eulerAngles = new Vector3(0f, 0f, heightr);
    }

    private void FixedUpdate()
    {
        if (collide)
        {
            float rand = Random.Range(0, 6);
            rb.useGravity = true;
            if(BirdDeath)
            {

                BirdDeath = false;
                if (winCondition)
                {
                    SoundControl.control.DeathBGM.mute = false;
                    SoundControl.control.DeathBGM.Stop();
                    if (rand < 2f)
                    {
                        SoundControl.control.DeathBGM.clip = SoundControl.control.ShowEm;
                    }
                    else if (rand < 4f)
                    {
                        SoundControl.control.DeathBGM.clip = SoundControl.control.GodsWork;
                    }
                    else
                    {
                        SoundControl.control.DeathBGM.clip = SoundControl.control.MissionAccomplished;
                    }
                    SoundControl.control.DeathBGM.PlayOneShot(SoundControl.control.DeathBGM.clip);
                    SoundControl.control.GameBGM.mute = true;
                    SoundControl.control.MainMenuBGM.mute = true;
                    SoundControl.control.CutSceneBGM.mute = true;
                }
                else
                {
                    SoundControl.control.DeathBGM.mute = false;
                    SoundControl.control.DeathBGM.Stop();
                    SoundControl.control.DeathBGM.PlayOneShot(SoundControl.control.DeathBGM.clip);
                    SoundControl.control.GameBGM.mute = true;
                    SoundControl.control.MainMenuBGM.mute = true;
                    SoundControl.control.CutSceneBGM.mute = true;


                    if (rand < 1f)
                    {
                        source.PlayOneShot(impact);
                        source.PlayOneShot(missionFail);
                    }
                    else if (rand < 2f)
                    {
                        source.PlayOneShot(splat);
                        source.PlayOneShot(missionFail);
                    }
                    else if (rand < 3f)
                    {
                        source.PlayOneShot(rattle);
                        source.PlayOneShot(missionFail);
                    }
                    else if (rand < 4f)
                    {
                        source.PlayOneShot(metal1);
                        source.PlayOneShot(missionFail);
                    }
                    else if (rand < 5f)
                    {
                        source.PlayOneShot(metal2);
                        source.PlayOneShot(missionFail);
                    }
                    else
                    {
                        source.PlayOneShot(thud);
                        source.PlayOneShot(missionFail);
                    }

                }
                
            }

           
            return;

        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        tilt = tilt - (vertical / 10);
        if (tilt > 1)
            tilt = 1f;
        if (tilt < 0)
            tilt = 0f;

        Rtilt = Rtilt + horizontal * 1.5f * extraTurning;
        if (Rtilt > 360)
            Rtilt = Rtilt - 360f;
        if (Rtilt < -1)
            Rtilt = 360f + Rtilt;
        
        float birdTilt = 0f;

        if (Input.GetKey("e"))
        {
            wasDive = true;
            tilt = -1.65f;
            birdTilt = (60f);
        }
        else
        {
            if (tilt < 0.5f)
            {
                birdTilt = (45f * (.5f - tilt));
            }
            else
            {
                birdTilt = ((tilt - .5f) * -45f);
            }
        }

        float lift = (tilt * 16f) - 10f;
        float fallSpeed = -lift;// - 9.8f;

        float momentum = fallSpeed * .05f;

        speed = speed + momentum;

        if (speed < 0.2f)
        {
            speed = 0.05f;
            lift = -1f;
            tilt = tilt - 0.05f;
        }
        else if (speed > maxspeed)
        {
            if (wasDive)
            {
                if (speed > diveMaxSpeed)
                {
                    speed = diveMaxSpeed;
                }
            }
            else
            {
                speed = maxspeed;
                wasDive = false;
            }
        }
        else
        {
            wasDive = false;
        }

        float velocity = (Mathf.Abs(speed) + 1) * 2;

        lift = lift + (thermalLift * extraThermalLift);
        

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

        int playSound = Random.Range(0, 2000);
        source.pitch = Random.Range(lowPitchRange, highPitchRange);
        float vol = Random.Range(volLowRange, volHighRange);
        if (playSound < 1 && speed < maxspeed / 2 && !source.isPlaying)
        {
            source.PlayOneShot(pigeonCoo, vol);
        }
        else if (playSound < 1 && speed < maxspeed && !source.isPlaying && speed > maxspeed / 2)
        {
            source.PlayOneShot(pigeon, vol);
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Thermal")
        {
            Debug.Log("Player hit a Thermal");
            thermalLift = other.gameObject.GetComponent<ThermalBehaviour>().getLift();
        }
        else
        {
            //Do nothing
        }
		
		//Use below to activate power-ups
		if (other.gameObject.tag == "PowerUp")
        {
            Debug.Log("Player got a Burger");
            GameManagerBehaviour.Instance.activatePowerUp(other.gameObject);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Thermal")
        {
            Debug.Log("Player exited a Thermal");
            thermalLift = 0f;
            if (!collision.gameObject.GetComponent<ThermalBehaviour>().isOnCooldown())
            {
                collision.gameObject.GetComponent<ThermalBehaviour>().goIntoCooldown();
            }
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