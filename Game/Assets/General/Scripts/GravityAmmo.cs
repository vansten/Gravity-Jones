﻿using UnityEngine;
using System.Collections;

public class GravityAmmo : MonoBehaviour {

    
    public float RotationSpeed = 15.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("AddGravityAmmo");
            Destroy(this.gameObject);
        }
    }
}