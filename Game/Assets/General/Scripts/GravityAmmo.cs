using UnityEngine;
using System.Collections;

public class GravityAmmo : MonoBehaviour {

    
    public float RotationSpeed = 15.0f;
    public AudioClip PickupSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            audio.PlayOneShot(PickupSound);
            col.gameObject.SendMessage("AddGravityAmmo");
            Destroy(this.gameObject);
        }
    }
}
