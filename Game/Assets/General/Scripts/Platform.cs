using UnityEngine;
using System.Collections;

public class Platform : LeverBasedObjectBase
{

    public GameObject Bridge;
    public GameObject Lava;
    public GameObject BridgeEndPoint;
    public float BridgeSpeed = 3.0f;

    private bool showBridge = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(showBridge)
        {
            if(Bridge.transform.position.z >= BridgeEndPoint.transform.position.z)
            {
                Bridge.transform.Translate(0, 0, -BridgeSpeed*Time.deltaTime);
            }
        }
	}

    public override void LeverOn()
    {
        Destroy(Lava.gameObject);
        showBridge = true;
    }

    public override void LeverOff()
    {

    }
}
