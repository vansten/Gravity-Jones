using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("Actual Level", 1);
		PlayerPrefs.SetInt("Artifacts Count", 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0))
		{
			int actualLevel = PlayerPrefs.GetInt ("Actual Level");
			if(actualLevel == 0){
				Application.LoadLevel(1);
			}
			else Application.LoadLevel(PlayerPrefs.GetInt ("Actual Level"));
		}
	}
}
