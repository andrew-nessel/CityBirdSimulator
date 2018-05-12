using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Target")
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

}
