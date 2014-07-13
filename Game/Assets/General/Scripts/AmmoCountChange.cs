using UnityEngine;
using System.Collections;

public class AmmoCountChange : MonoBehaviour {

	public Player Player;
	public GUIText Ammo;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Ammo.text = Player.GravityAmmo.ToString();
	}
}
