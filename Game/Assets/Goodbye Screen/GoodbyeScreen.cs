﻿using UnityEngine;
using System.Collections;

public class GoodbyeScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0) || Input.GetAxis("Trigger") < 0)
        {
            Application.LoadLevel(0);
        }
	}
}
