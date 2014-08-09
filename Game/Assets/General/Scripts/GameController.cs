using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static int ArtifactsCount = 0;
    public static int[] GravityAmmo = new int[3];
    [Range(0.0f, 1.0f)]
    public float AmbientLight = 5.0f / 255.0f;

	// Use this for initialization
	void Start () {
        ArtifactsCount = PlayerPrefs.GetInt("Artifacts Count");
		Screen.showCursor = false;
        LoadGravityAmmo();
        RenderSettings.ambientLight = new Color(AmbientLight, AmbientLight, AmbientLight);
	}

    public static int GetAmmoOnLevel()
    {
        return GravityAmmo[Application.loadedLevel - 1];
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.R) || Input.GetButton("Back"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if(Input.GetKey(KeyCode.Escape) || Input.GetButton ("Start"))
        {
            Application.Quit();
        }

#if UNITY_EDITOR
        Debug.Log("Unity Editor: " + Application.unityVersion);
        if(Input.GetKey(KeyCode.L) || Input.GetButton ("Left Button"))
        {
            int loadedLevel = Application.loadedLevel;
            if(loadedLevel != 4)
            {
                loadedLevel++;
            }
            else
            {
                loadedLevel = 0;
            }
            Application.LoadLevel(loadedLevel);
        }
        RenderSettings.ambientLight = new Color(AmbientLight, AmbientLight, AmbientLight);
#endif
	}

    void LoadGravityAmmo()
    {
        GravityAmmo[0] = 3;
        GravityAmmo[1] = 4;
        GravityAmmo[2] = 5;
    }
}
