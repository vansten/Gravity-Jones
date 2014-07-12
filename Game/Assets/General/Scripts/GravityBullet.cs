using UnityEngine;
using System.Collections;

public class GravityBullet : MonoBehaviour {

    public GameObject ForceCenter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet" || col.gameObject.tag == "EnemyDyingBox" || col.gameObject.tag == "Lever")
        {
            col.gameObject.SendMessage("Defy", ForceCenter);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet" || col.gameObject.tag == "EnemyDyingBox" || col.gameObject.tag == "Lever")
        {
            col.gameObject.SendMessage("StopDefying");
        }
    }
}
