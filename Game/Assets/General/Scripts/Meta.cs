using UnityEngine;
using System.Collections;

public class Meta : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            int loadedLevel = Application.loadedLevel;
			PlayerPrefs.SetInt("Artifacts Count", GameController.ArtifactsCount);
            int levelCount = Application.levelCount;
            if(loadedLevel < levelCount - 1)
            {
                Application.LoadLevel(loadedLevel + 1);
            }
            else
            {
                Debug.Log("There's no more levels");
                Application.LoadLevel(0);
            }
        }
    }
}
