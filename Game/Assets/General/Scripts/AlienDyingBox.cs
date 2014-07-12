using UnityEngine;
using System.Collections;

public class AlienDyingBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Die()
    {
        this.transform.parent.gameObject.SendMessage("Die");
        this.collider2D.enabled = false;
    }

    void Defy(GameObject ForceCenter)
    {
        transform.parent.gameObject.SendMessage("Defy", ForceCenter);
    }

    void StopDefying()
    {
        transform.parent.gameObject.SendMessage("StopDefying");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 9)
        {
            Debug.Log("kolizja ze sciana");
            Destroy(rigidbody2D);
        }
    }
}
