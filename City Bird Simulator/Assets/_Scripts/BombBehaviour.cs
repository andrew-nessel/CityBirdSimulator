using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

    public int predictionStepsPerFrame = 12;

    public Vector3 bombVelocity;
    public float speed; 
    float stepSize = 0.01f;
    public Rigidbody r;
    public GameObject GameManager;
    public GameObject BombCamera;
	public GameObject SplatParticles;
	
    //Sound variables
    public AudioClip shoot1;
    public AudioClip shoot2;
    

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        r = GetComponent<Rigidbody>();
        //speed = GetComponent<PlayerBehaviour>().speed;
        //r.velocity = GetComponent<PlayerBehaviour>().rb.velocity;
        float velocity = ((Mathf.Abs(speed) + 1) * 2);
        //bombVelocity = GetComponent<PlayerBehaviour>().transform.forward * velocity;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        //Play sound upon
        float vol = Random.Range(volLowRange, volHighRange);
        float rand = Random.Range(0, 2);
        if(rand < 1f)
        {
            source.PlayOneShot(shoot1, vol);
        }
        else if(rand < 2f)
        {
            source.PlayOneShot(shoot2, vol);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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
    
        BombCamera.transform.position = currentPosition - new Vector3(transform.forward.x * 10, -1f, transform.forward.z * 10);


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

        if (this.transform.position.y < -100)
        {
            Destroy(gameObject);
            GameManager.GetComponent<GameManagerBehaviour>().deactivateBomb();
        }
    }

    IEnumerator SplatSoundDelay()
    {
        yield return new WaitForSeconds(5f);
    }
    void OnCollisionEnter(Collision collision)
    {
        
        GetComponent<Renderer>().enabled = false;
        if (collision.gameObject.tag == "Target")
        {
            Debug.Log("Bomb hit a TARGET");
			Instantiate(this.SplatParticles, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
            collision.gameObject.tag = "HitTarget";
            GameManager.GetComponent<GameManagerBehaviour>().deactivateBomb();
            GameManager.GetComponent<GameManagerBehaviour>().UpdateTargets(GameManager.GetComponent<GameManagerBehaviour>().Targets + 1);
            GameManager.GetComponent<GameManagerBehaviour>().checkForGoal();
        }
        else if (collision.gameObject.tag == "Bomb")
        {
            //Do nothing
        }
        else
        {
            Debug.Log("Bomb hit something else");
			Instantiate(this.SplatParticles, this.transform.position, Quaternion.identity);

            Destroy(gameObject);
            GameManager.GetComponent<GameManagerBehaviour>().deactivateBomb();
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
