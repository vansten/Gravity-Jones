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
        if (spr == null)
        {
            Debug.LogException(new MissingComponentException("This game object has to have SpriteRenderer component!"));
            Debug.DebugBreak();
        }
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
            }
            else
            {
                this.transform.Translate(0, 2, 0);
            }
            isAffectedAlready = true;
        }
    }

    void StopDefying()
    {
        if (isAffectedAlready)
        {
            target.LeverOff();
            spr.sprite = LeverOff;
            if(Horizontal)
            {
                this.transform.Translate(-2, 0, 0);
            }
            else
            {
                this.transform.Translate(0, -2, 0);
            }
            isAffectedAlready = false;
        }
    }
}
