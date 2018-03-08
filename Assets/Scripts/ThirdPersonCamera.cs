using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	Camera cam;

    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target;
    public float disFromTarget = 2.5f;
    public Vector2 pitchMinMax = new Vector2 (-40, 85);

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothTimeVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;
	
    void Start()
    {
		cam = GetComponent<Camera>();

        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

	void Update()
	{
		/*Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
			print("I'm looking at " + hit.transform.name);
		else
			print("I'm looking at nothing!"); */
	}

	// Update is called once per frame
	void LateUpdate ()
    {
        yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3(pitch, yaw), ref rotationSmoothTimeVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * disFromTarget;
	}
}
