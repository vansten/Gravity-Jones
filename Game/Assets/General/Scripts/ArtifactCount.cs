using UnityEngine;
using System.Collections;

public class ArtifactCount : MonoBehaviour {

	public GUIText Artifact;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Artifact.text = GameController.ArtifactsCount.ToString();
	}
}
