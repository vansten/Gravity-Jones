using UnityEngine;
using System.Collections;

public class ActualLevelText : MonoBehaviour {

	public GUIText ActualLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ActualLevel.text = Application.loadedLevel.ToString ();
	}
}
