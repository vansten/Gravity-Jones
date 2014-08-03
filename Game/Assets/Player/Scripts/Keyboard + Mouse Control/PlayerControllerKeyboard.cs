using UnityEngine;
using System.Collections;

public class PlayerControllerKeyboard : Player {

	// Use this for initialization
	void Start ()
    {
        myCamera = Camera.main;
        isAlive = true;
        myCamera.transform.position = this.transform.position;
        myCamera.transform.Translate(new Vector3(0, 0, -20.0f));
        anim = this.gameObject.GetComponent<Animator>();
        this.GravityAmmo = GameController.GetAmmoOnLevel();
        audio = this.transform.GetComponent<AudioSource>();
        RightStick.transform.position = this.transform.position + new Vector3(0, 1, 0);
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
                audio.Stop();
            }
            else
            {
                if (!audio.isPlaying)
                {
                    audio.Play();
                }
            }

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f;
            Vector3 lookPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            lookPosition.z = 0;
            Vector3 lookRot = lookPosition - this.transform.position;
            if (lookRot.x == 0)
            {
                lookRot.x = 0.01f;
            }
            this.transform.rotation = Quaternion.LookRotation(lookPosition - this.transform.position);
            this.transform.Rotate(new Vector3(0, 1, 0), 90);
            this.transform.Rotate(new Vector3(0, 0, -1), 90);
            if (lookRot.x > 0.0f)
            {
                this.transform.Rotate(new Vector3(0, 1, 0), 180);
            }

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
