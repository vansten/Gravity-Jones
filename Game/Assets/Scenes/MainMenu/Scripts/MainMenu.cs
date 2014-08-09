using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    public List<GameObject> Options = new List<GameObject>();
    public GameObject InstructionsSprite;
    public GameObject CreditsSprite;

    private int chosenOption = 0;
    private bool instructionsOrCredits = false;
    private bool changed = false;
    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
        if (Options.Count > 0)
        {
            chosenOption = 0;
            Options[chosenOption].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            Debug.LogError("You have to add some Options!");
            Debug.Break();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!instructionsOrCredits)
        {
            if (changed)
            {
                timer += Time.deltaTime;
                if(timer > 1.0f)
                {
                    changed = false;
                    timer = 0.0f;
                }
            }
            else
            {
                float axis = Input.GetAxis("Vertical");
                if (axis > 0.0f)
                {
                    GoUp();
                }
                else if (axis < 0.0f)
                {
                    GoDown();
                }
            }
            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Joystick1Button0))
            {
                if (chosenOption == 0)
                {
                    Application.LoadLevel("Level1");
                }
                else if (chosenOption == 1)
                {
                    InstructionsSprite.renderer.sortingLayerName = "Blood";
                    instructionsOrCredits = true;
                }
                else if (chosenOption == 2)
                {
                    CreditsSprite.renderer.sortingLayerName = "Blood";
                    instructionsOrCredits = true;
                }
                else if (chosenOption == 3)
                {
                    Application.Quit();
                }
            }
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Joystick1Button1))
            {
                instructionsOrCredits = false;
                CreditsSprite.renderer.sortingLayerName = "Default";
                InstructionsSprite.renderer.sortingLayerName = "Default";
            }
        }

        if(CreditsSprite.renderer.sortingLayerName == "Blood" || InstructionsSprite.renderer.sortingLayerName == "Blood")
        {
            instructionsOrCredits = true;
        }
	}

    void GoUp()
    {
        if(chosenOption == 0)
        {
            return;
        }
        Options[chosenOption].GetComponent<SpriteRenderer>().color = Color.green;
        chosenOption--;
        Options[chosenOption].GetComponent<SpriteRenderer>().color = Color.yellow;
        changed = true;
    }

    void GoDown()
    {
        if (chosenOption == 3)
        {
            return;
        }
        Options[chosenOption].GetComponent<SpriteRenderer>().color = Color.green;
        chosenOption++;
        Options[chosenOption].GetComponent<SpriteRenderer>().color = Color.yellow;
        changed = true;
    }
}
