using UnityEngine;
using System.Collections;

public class RightStickMouse : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (PlayerControllerKeyboard.isAlive)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
            position.z = 0.0f;
            this.gameObject.transform.position = position;
        }
	}
}
