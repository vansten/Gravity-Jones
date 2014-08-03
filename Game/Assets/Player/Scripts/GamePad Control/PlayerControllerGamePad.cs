using UnityEngine;
using System.Collections;

public class PlayerControllerGamePad : Player {

	// Use this for initialization
	void Start ()
    {
        myCamera = Camera.main;
        isAlive = true;
        myCamera.transform.position = this.transform.position;
        myCamera.transform.Translate(new Vector3(0, 0, -20.0f));
        anim = this.gameObject.GetComponent<Animator>();
        this.GravityAmmo = GameController.GetAmmoOnLevel();
        audio = this.transform.GetComponents<AudioSource>();
        RightStick.transform.position = this.transform.position + new Vector3(0, 1, 0);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(isAlive)
        {
            myCamera.transform.position = this.transform.position;
            myCamera.transform.Translate(new Vector3(0, 0, -20.0f));
            float X = Input.GetAxis("Horizontal");
            float Y = Input.GetAxis("Vertical");
            if (X != 0 || Y != 0)
            {
                isWalking = true;
                this.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime, Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime, 0), Space.World);
                RightStick.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime, Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime, 0), Space.World);
            }
            else
            {
                isWalking = false;
            }
            anim.SetBool("Is walking", isWalking);
            if (!isWalking)
            {
                audio[0].Stop();
            }
            else
            {
                if (!audio[0].isPlaying)
                {
                    audio[0].Play();
                }
            }

            Vector3 lookRot = RightStick.transform.position - this.transform.position;
            if (lookRot.x == 0)
            {
                lookRot.x = 0.01f;
            }
            this.transform.up = -lookRot.normalized;

            if (Input.GetAxis("Trigger") < 0 && Input.GetAxis("Trigger") > -0.7)
            {
                Shoot();
            }
            if (Input.GetAxis("Trigger") > 0 && Input.GetAxis("Trigger") < 0.7)
            {
                ShootGravity(RightStick.transform.position);
            }
            if (Input.GetAxis("Trigger") == 0 || Input.GetAxis("Trigger") > 0.7 || Input.GetAxis("Trigger") < -0.7)
            {
                startCounting = true;
            }

            if (startCounting)
            {
                timer += Time.deltaTime;
                if (timer > Cooldown)
                {
                    canShoot = true;
                    startCounting = false;
                    timer = 0.0f;
                }
            }
        }
        else
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= 0.4f)
            {
                Blood.renderer.enabled = true;
            }
            if (deathTimer >= deathCooldown)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
	}
}
