using UnityEngine;
using System.Collections;

public class Log : LeverBasedObjectBase {

    public GameObject StartPoint;
    public GameObject EndPoint;
    public float Speed = 50.0f;

    private bool fromStartToEnd = true;
    private bool affected = false;
    private bool switchOn = false;
    private GameObject ForceCenter;
    private bool canKillPlayer = true;

	// Use this for initialization
	void Start () {
        this.transform.position = StartPoint.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (affected == false)
        {
            if (switchOn == false)
            {
                if (fromStartToEnd)
                {
                    this.transform.Translate(Vector3.down * Speed * Time.deltaTime);
                    if (this.transform.position.x >= EndPoint.transform.position.x)
                    {
                        fromStartToEnd = false;
                    }
                }
                else
                {
                    this.transform.Translate(Vector3.up * Speed * Time.deltaTime);
                    if (this.transform.position.x <= StartPoint.transform.position.x)
                    {
                        fromStartToEnd = true;
                    }
                }
            }
        }
        else if(affected)
        {
           if(this.transform.position.x < ForceCenter.transform.position.x)
           {
               this.transform.position = StartPoint.transform.position;
           }
           else
           {
               this.transform.position = EndPoint.transform.position;
           }
        }
	}

    void Defy(GameObject ForceCenter)
    {
        this.affected = true;
        this.canKillPlayer = false;
        this.ForceCenter = ForceCenter;
    }

    void StopDefying()
    {
        //this.affected = false;
    }

    public override void LeverOn()
    {
        this.switchOn = true;
        this.canKillPlayer = false;
        Vector3 distanceToStart = this.transform.position - StartPoint.transform.position;
        Vector3 distanceToEnd = this.transform.position - EndPoint.transform.position;
        float distanceToStartLength = distanceToStart.magnitude;
        float distanceToEndLength = distanceToEnd.magnitude;
        if (distanceToStartLength > distanceToEndLength)
        {
            this.transform.position = EndPoint.transform.position;
        }
        else
        {
            this.transform.position = StartPoint.transform.position;
        }
    }

    public override void LeverOff()
    {
        this.switchOn = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "EnemyDyingBox")
        {
            if (canKillPlayer)
            {
                Vector3 force = col.transform.position - this.transform.position;
                force.z = 0.0f;
                Vector3 forcePos = col.contacts[0].collider.transform.position;
                forcePos.z = 0.0f;
                col.rigidbody.AddForceAtPosition(300.0f * force, forcePos);
                col.gameObject.SendMessage("Die", "Log");
            }
        }
    }
}
