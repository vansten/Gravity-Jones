using UnityEngine;
using System.Collections;

public class InstructionsButton : MonoBehaviour {

    public GameObject InstructionsSprite;

    private bool mouseOver = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(mouseOver && Input.GetMouseButtonUp(0))
        {
            InstructionsSprite.renderer.sortingLayerName = "Blood";
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
        mouseOver = false;
    }
}
