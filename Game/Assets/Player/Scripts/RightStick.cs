using UnityEngine;
using System.Collections;

public class RightStick : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		string[] inputArray = Input.GetJoystickNames ();
        if (inputArray.Length == 0)
        {
            this.GetComponent<RightStickMouse>().enabled = true;
        }
        else
        {
            this.GetComponent<RightStickGamePad>().enabled = true;
        }
        this.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}
}
