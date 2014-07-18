using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 5.0f;
    public float bulletLifetime = 3.0f;

    private GameObject ForceCenter;
    private bool isAffected = false;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, bulletLifetime);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
        
        if(this.isAffected)
        {
            Vector2 forceDirection = this.transform.position - ForceCenter.transform.position;
            forceDirection *= 250.0f / forceDirection.magnitude;
            this.rigidbody2D.AddForce(forceDirection, ForceMode2D.Force);
        }
	}

    void Defy(GameObject ForceCenter)
    {
        this.ForceCenter = ForceCenter;
        this.isAffected = true;
    }

    void StopDefying()
    {
        this.isAffected = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyDyingBox")
        {
            col.gameObject.SendMessage("Die", "Bullet");
            Destroy(this.gameObject);
        }
        if(col.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("Die", "Bullet");
        }
    }
}
