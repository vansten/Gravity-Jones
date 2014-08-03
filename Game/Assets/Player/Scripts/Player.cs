using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float MovementSpeed = 25.0f;
	public GameObject Bullet;
	public float Cooldown = 0.5f;
	public GameObject GravityBullet;
	public int GravityAmmo = 5;
	public GameObject MyArm;
	public AudioClip GravitySound;
	public AudioClip ShootSound;
	public GameObject Blood;
	public AudioClip WalkSound;
    public GameObject RightStick; 
    public AudioClip DeathSound;
		
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
    protected Camera myCamera;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    protected void Shoot()
    {
        if (canShoot)
        {
            GameObject bullet = Instantiate(Bullet, MyArm.transform.position, this.transform.rotation) as GameObject;
            bullet.transform.Rotate(0, 0, 180);
            audio[1].PlayOneShot(ShootSound);
            canShoot = false;
        }
    }

    protected void ShootGravity(Vector3 lookPosition)
    {
        if (canShoot)
        {
            if (GravityAmmo > 0)
            {
                Instantiate(GravityBullet, lookPosition, this.transform.rotation);
                audio[2].PlayOneShot(GravitySound);
                canShoot = false;
                GravityAmmo--;
            }
        }
    }

    void AddGravityAmmo()
    {
        GravityAmmo++;
    }

    void AddArtifact()
    {
        GameController.ArtifactsCount++;
    }

    void Die(string cause)
    {
        if (isAlive)
        {
            isAlive = false;
            anim.Play("Death");
            audio[3].PlayOneShot(DeathSound);
            if (cause != "Log")
            {
                collider2D.enabled = false;
            }
        }
    }
}
