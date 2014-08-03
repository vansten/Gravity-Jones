using UnityEngine;
using System.Collections;

public class RightStickGamePad : MonoBehaviour {

    public float MovementSpeed = 3.0f;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (PlayerControllerGamePad.isAlive)
        {
            this.transform.Translate(new Vector3(Input.GetAxis("Joy X") * MovementSpeed * Time.deltaTime, Input.GetAxis("Joy Y") * -MovementSpeed * Time.deltaTime, 0), Space.World);
        }
	}
}
