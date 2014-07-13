using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float MovementSpeed = 3.0f;
	public GameObject Bullet;
	public float Cooldown = 0.5f;
	public GameObject GravityBullet;
	public int GravityAmmo = 5;
	public GameObject MyArm;
	public GameObject myCamera;
	public AudioClip GravitySound;
	public AudioClip ShootSound;
	public GameObject PlanePrefab;
	public AudioClip WalkSound;
	public GameObject RightStick;
		
	protected bool canShoot = true;
	protected bool startCounting = false;
	protected float timer = 0.0f;
	protected bool isWalking = false;
	protected Animator anim;
	public static bool isAlive = true;
	protected float deathTimer = 0.0f;
	protected float deathCooldown = 2.0f;
	protected bool planeDone = false;
	protected new AudioSource[] audio;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
