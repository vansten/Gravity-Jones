using UnityEngine;
using System.Collections;

public class PlayerController : Player {

	private bool isPadPlugged = false;

	// Use this for initialization
	void Start () 
    {
		isAlive = true;
		myCamera.transform.position = this.transform.position;
		myCamera.transform.Translate(new Vector3(0, 0, -20.0f));
        anim = gameObject.GetComponent<Animator>();
        this.GravityAmmo = GameController.GetAmmoOnLevel();
        audio = this.transform.GetComponents<AudioSource>();
		string[] inputArray = Input.GetJoystickNames ();
		if (inputArray.Length == 0){
			isPadPlugged = false;
		}
		else isPadPlugged = true;
        RightStick.transform.position = this.transform.position + new Vector3(0,0,4);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isAlive)
        {
            myCamera.transform.position = this.transform.position;
            myCamera.transform.Translate(new Vector3(0, 0, -20.0f));
			if(isPadPlugged)
			{
				float X = Input.GetAxis ("Horizontal");
				float Y = Input.GetAxis ("Vertical");
				if (X != 0 || Y != 0)
				{
					isWalking = true;
					this.transform.Translate(new Vector3(Input.GetAxis ("Horizontal") * MovementSpeed * Time.deltaTime, Input.GetAxis ("Vertical") * MovementSpeed * Time.deltaTime, 0), Space.World);
					RightStick.transform.Translate(new Vector3(Input.GetAxis ("Horizontal") * MovementSpeed * Time.deltaTime, Input.GetAxis ("Vertical") * MovementSpeed * Time.deltaTime, 0), Space.World);
				}
				else
				{
					isWalking = false;
				}
				anim.SetBool("Is walking", isWalking);

				Vector3 lookPosition = RightStick.transform.position;
                Vector3 lookRot = lookPosition - this.transform.position;
                if(lookRot.x == 0)
                {
                    lookRot.x = 0.01f;
                }
				this.transform.rotation = Quaternion.LookRotation(lookRot, Vector3.up);
				this.transform.Rotate(new Vector3(0, 1, 0), 90);
				this.transform.Rotate(new Vector3(0, 0, -1), 90);
				/*
				if (Input.GetButtonDown("Fire1"))
				{
					Shoot();
				}
				if (Input.GetButtonUp("Fire1"))
				{
					startCounting = true;
				}
				if (Input.GetButtonDown("Fire3"))
				{
					ShootGravity(lookPosition);
				}
				if (Input.GetButtonUp("Fire3"))
				{
					startCounting = true;
				}
				*/
				if(Input.GetAxis("Trigger") < 0)
				{
					Shoot ();
				}
				if(Input.GetAxis ("Trigger") > 0)
				{
					ShootGravity(lookPosition);
				}
				if(Input.GetAxis("Trigger") == 0)
				{
					startCounting = true;
				}
			}
			else
			{
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
				this.transform.rotation = Quaternion.LookRotation(lookPosition - this.transform.position);
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

	void AddArtifact()
	{
		GameController.ArtifactsCount++;
	}

    void Die()
    {
        isAlive = false;
        anim.Play("Death");
    }
}
