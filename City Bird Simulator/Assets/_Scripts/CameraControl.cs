using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject player;
    

    public Transform target;
    public float distance = 10f;
    public float xSpeed = 100.0f;
    public float ySpeed = 100.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = 10f;
    public float distanceMax = 20f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        target = player.transform;

        //rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        //if (rigidbody != null)
        //{
            //rigidbody.freezeRotation = true;
        //}
    }

    void LateUpdate()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                if(!(hit.collider.gameObject.tag == "Bomb" || hit.collider.gameObject.tag == "Thermal"))
                {
                    distance -= hit.distance;
                    if (distance <= 3f)
                    {
                        distance = 3f;
                    }
                }
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
