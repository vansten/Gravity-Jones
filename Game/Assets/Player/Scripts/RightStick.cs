using UnityEngine;
using System.Collections;

public class RightStick : MonoBehaviour {

	public float MovementSpeed = 3.0f;

	private bool isPadPlugged = false;

	// Use this for initialization
	void Start () {
		string[] inputArray = Input.GetJoystickNames ();
		if (inputArray.Length == 0){
			isPadPlugged = false;
		}
		else isPadPlugged = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerController.isAlive){
			this.transform.Translate(new Vector3(Input.GetAxis ("Joy X") * MovementSpeed * Time.deltaTime, Input.GetAxis ("Joy Y") * -MovementSpeed * Time.deltaTime, 0), Space.World);
			if(isPadPlugged)
			{
				this.transform.Translate(new Vector3(Input.GetAxis ("Joy X") * MovementSpeed * Time.deltaTime, Input.GetAxis ("Joy Y") * -MovementSpeed * Time.deltaTime, 0), Space.World);
			}
			else
			{
				Vector3 mousePosition = Input.mousePosition;
				Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
				position.z = 0.0f;
				this.transform.position = position;
			}
		}
	}
}
