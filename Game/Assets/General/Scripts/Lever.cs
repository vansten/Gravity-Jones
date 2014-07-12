using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public LeverBasedObjectBase target;
    public Sprite LeverOn;
    public Sprite LeverOff;

    public bool Horizontal = false;

    private SpriteRenderer spr;
    private bool isAffectedAlready = false;

	// Use this for initialization
	void Start () {
        spr = this.transform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void Defy(GameObject ForceCenter)
    {
        if (!isAffectedAlready)
        {
            target.LeverOn();
            spr.sprite = LeverOn;
            if(Horizontal)
            {
                this.transform.Translate(2, 0, 0);
                //this.transform.Rotate(new Vector3(0, 0, 1), 180);
            }
            else
            {
                this.transform.Translate(0, 2, 0);
                //this.transform.Rotate(new Vector3(0, 0, 1), 180);
            }
            isAffectedAlready = true;
        }
    }

    void StopDefying()
    {

    }
}
