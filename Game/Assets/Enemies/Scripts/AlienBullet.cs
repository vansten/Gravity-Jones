using UnityEngine;
using System.Collections;

public class AlienBullet : MonoBehaviour {

    public float BulletSpeed = 15.0f;
    public float BulletLifetime = 5.0f;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, BulletLifetime);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(0, -BulletSpeed * Time.deltaTime, 0);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("Die", "Alien Bullet");
            Destroy(this.gameObject);
        }
    }
}
