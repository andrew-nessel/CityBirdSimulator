using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject player;
    public float horizontalSpeed = 2f;
    public float verticalSpeed = 2f;

    private float yaw;
    private float pitch;

    private Vector3 initialOffset;
    private Vector3 offset;

    // Use this for initialization
    void Start () {
        initialOffset = transform.position - player.transform.position;
        offset = initialOffset;
        pitch = 0f;
        yaw = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * horizontalSpeed, Vector3.up) * offset;
        transform.position = player.transform.position + offset;

        if (Input.GetKey("r"))
        {
            offset = Quaternion.AngleAxis(player.transform.eulerAngles.y, Vector3.up) * initialOffset;
            transform.position = player.transform.position + offset;
        }

        transform.LookAt(player.transform.position);

	}
}
