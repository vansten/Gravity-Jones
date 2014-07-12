﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static int ArtifactsCount = 0;

	// Use this for initialization
	void Start () {
        ArtifactsCount = PlayerPrefs.GetInt("Artifacts Count");
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        PlayerPrefs.SetInt("Artifacts Count", ArtifactsCount);
    }
}