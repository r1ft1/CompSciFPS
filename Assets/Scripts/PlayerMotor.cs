using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera cam;

	private Vector3 velocity;
	private Vector3 rotation;
	private Vector3 cameraRotation;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}


	//Gets movement vector from PlayerController
	public void Move(Vector3 _velocty)
	{
		velocity = _velocty;
	}

	//Gets rotational vector from PlayerController
	public void Rotate(Vector3 _rotatation)
	{
		rotation = _rotatation;
	}

	//Gets rotational vector for camera from PlayerController
	public void RotateCamera(Vector3 _cameraRotation)
	{
		cameraRotation = _cameraRotation;
	}

	void FixedUpdate()
	{
		PerformMovement ();
		PerformRotation ();
	}

	//Performs Movement based on velocty variable
	void PerformMovement()
	{
		if (velocity != Vector3.zero) {
			rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
		}
	}

	void PerformRotation()
	{
		rb.MoveRotation (rb.rotation * Quaternion.Euler (rotation));
		if (cam != null)
			cam.transform.Rotate (-cameraRotation);
	}
}
