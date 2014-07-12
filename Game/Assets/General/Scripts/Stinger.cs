using UnityEngine;
using System.Collections;

public class Stinger : MonoBehaviour {

    private float timer = 0.0f;
    private float cooldown = 0.3f;
    private GameObject other;
    private bool dying = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(dying)
        {
            timer += Time.deltaTime;
            if(timer >= cooldown)
            {
                other.gameObject.SendMessage("Die");
                dying = false;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "EnemyDyingBox")
        {
            other = col.gameObject;
            dying = true;
        }
    }
}
