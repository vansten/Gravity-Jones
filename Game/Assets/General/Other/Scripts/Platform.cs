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
            if (renderer.sortingOrder < 1)
            {
                renderer.sortingOrder += 1;
            }
            else
            {
                this.enabled = false;
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
