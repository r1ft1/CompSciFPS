using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField] //Allows variable to show in editor 
	private float speed = 10f;
	[SerializeField]
	private float lookSensitivity = 3f;

	private PlayerMotor motor;
	

	void Start ()
	{
		motor = GetComponent<PlayerMotor> ();
	}

	void Update()
	{
		//Calc Movement as 3D vector
		float _xMove = Input.GetAxisRaw ("Horizontal"); // Can be 1, -1 or 0
		float _zMove = Input.GetAxisRaw ("Vertical");

		Vector3 _moveHorizontal = transform.right * _xMove;   // (1, 0, 0) * 1, -1, 0 if right/left/not 
		Vector3 _moveVertical = transform.forward * _zMove;   // (0, 0, 1) * 1, -1, 0 if forward/backward/not 

		//Final Movement vector
		//Normalize returns the combined vector to have length 1, to get direction
		Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;    //Direction * speed

		//Apply Movement
		motor.Move(_velocity);

		//Calc rotation as 3D vector (Player Turning around)
		float _yRot = Input.GetAxisRaw ("Mouse X");  //Rotating around the Y Axis

		Vector3 _rotation = new Vector3 (0f, _yRot, 0f) * lookSensitivity;

		//Applying Rotation
		motor.Rotate(_rotation);

		//Calc camera rotation as 3D vector (Camera Looking around)
		float _xRot = Input.GetAxisRaw ("Mouse Y");   //Rotating around the X Axis
		//Rotating the camera instead of the Player to prevent upwards movement
		Vector3 _cameraRotation = new Vector3 (_xRot, 0f, 0f) * lookSensitivity;  

		//Applying camera Rotation
		motor.RotateCamera(_cameraRotation);
	}
}
