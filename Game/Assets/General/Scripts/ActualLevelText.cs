using UnityEngine;
using System.Collections;

public class ActualLevelText : MonoBehaviour {

	public GUIText ActualLevel;
	public Texture2D Crosshair;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ActualLevel.text = Application.loadedLevel.ToString ();
	}

	void OnGUI(){
		//Vector3 tmp = Camera.main.ScreenToWorldPoint (Input.mousePosition).normalized;
		GUI.DrawTexture (new Rect (Input.mousePosition.x - (Crosshair.width / 2), (Screen.height - Input.mousePosition.y) - (Crosshair.height / 2), Crosshair.width, Crosshair.height), Crosshair);
	}
}
