using UnityEngine;
using System.Collections;

public class AlienDyingBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Die(string cause)
    {
        this.transform.parent.gameObject.SendMessage("Die", cause);
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
        if(col.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(rigidbody2D);
        }
    }
}
