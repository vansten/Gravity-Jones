using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    

	// Use this for initialization
	void Start () 
    {
		string[] inputArray = Input.GetJoystickNames ();
        if (inputArray.Length == 0)
        {
            this.gameObject.GetComponent<PlayerControllerKeyboard>().enabled = true;
            this.enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<PlayerControllerGamePad>().enabled = true;
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
}
