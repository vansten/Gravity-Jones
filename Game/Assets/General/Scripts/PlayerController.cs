using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

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

    private bool canShoot = true;
    private bool startCounting = false;
    private float timer = 0.0f;
    private bool isWalking = false;
    private Animator anim;
    private bool isAlive = true;
    private float deathTimer = 0.0f;
    private float deathCooldown = 2.0f;
    private bool planeDone = false;
    private new AudioSource[] audio;

	// Use this for initialization
	void Start () 
    {
        anim = gameObject.GetComponent<Animator>();
        this.GravityAmmo = GameController.GetAmmoOnLevel();
        audio = this.transform.GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isAlive)
        {
            myCamera.transform.position = this.transform.position;
            myCamera.transform.Translate(new Vector3(0, 0, -20.0f));
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(new Vector3(-MovementSpeed * Time.deltaTime, 0, 0), Space.World);
                isWalking = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(new Vector3(MovementSpeed * Time.deltaTime, 0, 0), Space.World);
                isWalking = true;
            }
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.Translate(new Vector3(0, MovementSpeed * Time.deltaTime, 0), Space.World);
                isWalking = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.transform.Translate(new Vector3(0, -MovementSpeed * Time.deltaTime, 0), Space.World);
                isWalking = true;
            }
            if (Input.anyKey == false)
            {
                isWalking = false;
            }
            anim.SetBool("Is walking", isWalking);
            if (!isWalking)
            {
                audio[2].Stop();
            }
            else
            {
                if (!audio[2].isPlaying)
                {
                    audio[2].Play();
                }
            }

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f;
            Vector3 lookPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            lookPosition.z = 0;
            this.transform.LookAt(lookPosition);
            this.transform.Rotate(new Vector3(0, 1, 0), 90);
            this.transform.Rotate(new Vector3(0, 0, -1), 90);

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
            if (Input.GetMouseButtonUp(0))
            {
                startCounting = true;
            }
            if (Input.GetMouseButtonDown(1))
            {
                ShootGravity(lookPosition);
            }
            if (Input.GetMouseButtonUp(1))
            {
                startCounting = true;
            }

            if (startCounting)
            {
                timer += Time.deltaTime;
                if (timer >= Cooldown)
                {
                    canShoot = true;
                    timer = 0.0f;
                    startCounting = false;
                }
            }
        }
        else
        {
            deathTimer += Time.deltaTime;
            if(deathTimer>=0.4f)
            {
                if (planeDone == false)
                {
                    Instantiate(PlanePrefab, this.transform.position + new Vector3(0,0,1), PlanePrefab.transform.rotation);
                    planeDone = true;
                }
            }
            if(deathTimer >= deathCooldown)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
	}

    void ShootGravity(Vector3 lookPosition)
    {
        if(canShoot)
        {
            if (GravityAmmo > 0)
            {
                Instantiate(GravityBullet, lookPosition, this.transform.rotation);
				audio[0].PlayOneShot(GravitySound);
                canShoot = false;
                GravityAmmo--;
            }
        }
    }

    void Shoot()
    {
        if (canShoot)
        {
            Instantiate(Bullet, MyArm.transform.position, this.transform.rotation);
			audio[1].PlayOneShot(ShootSound);
            canShoot = false;
        }
    }

    void AddGravityAmmo()
    {
        GravityAmmo++;
    }

    void Die()
    {
        isAlive = false;
        anim.Play("Death");
        this.collider2D.enabled = false;
    }
}
