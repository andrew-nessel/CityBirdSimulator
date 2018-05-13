using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {
    public float speed = 420.0f;
    public int predictionStepsPerFrame = 6;
    public Vector3 bombVelocity;
    // Use this for initialization
    void Start () {
        bombVelocity = this.transform.forward * speed;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void OnCollisionEnter(Collision collision)
    {
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
        this.transform.position = point1;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float stepSize = 0.01f;
        Vector3 point1 = this.transform.position;
        Vector3 predictedBombVelocity = bombVelocity;
        for (float step = 0; step < 1; step += stepSize)
        {
            predictedBombVelocity += Physics.gravity * stepSize;
            Vector3 point2 = point1 + bombVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }

}
