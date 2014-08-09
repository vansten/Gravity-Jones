using UnityEngine;
using System.Collections;

public class CreditsButton : MonoBehaviour {
    
    public GameObject CreditsSprite;

    private bool mouseOver = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (mouseOver && Input.GetMouseButtonUp(0))
        {
            CreditsSprite.renderer.sortingLayerName = "Blood";
        }
	}

    void OnMouseEnter()
    {
        this.GetComponent<SpriteRenderer>().color = Color.yellow;
        mouseOver = true;
    }

    void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().color = Color.green;
        mouseOver = true;
    }
}
