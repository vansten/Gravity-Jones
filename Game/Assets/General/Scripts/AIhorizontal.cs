using UnityEngine;
using System.Collections;

public class AIhorizontal : MonoBehaviour {

    public GameObject StartPoint;
    public GameObject EndPoint;
    public float speed = 5.0f;
    public GameObject Bullet;
    public float Cooldown;
    public GameObject MyArm;
	public AudioClip ShootSound;
    public GameObject BloodPlane;
    public GameObject AlienGunFakeParticle;

    private Vector3 direction;
    private bool fromStartToEnd = true;
    private bool walk = true;
    private Animator anim;
    private GameObject target;
    private bool canShoot = true;
    private bool startCounting = false;
    private float timer = 0.0f;
    private GameObject ForceCenter;
    private bool affected = false;
    private bool isAlive = true;
    private float deathTimer = 0.0f;
    private float fakeParticleTimer = 0.0f;
    private float fakeParticleCooldown = 0.2f;
    private bool doAlienGunFakeParticle = false;

	// Use this for initialization
	void Start () {
        this.transform.position = StartPoint.transform.position;
        anim = GetComponent<Animator>();
        if(StartPoint.transform.position == EndPoint.transform.position)
        {
            walk = false;
        }
        AlienGunFakeParticle.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isAlive)
        {
            if (affected)
            {
                Vector3 tmp = this.transform.position - ForceCenter.transform.position;
                Vector2 forceDirection = new Vector2(tmp.x, tmp.y);
                forceDirection *= 75.0f / forceDirection.magnitude;
                this.rigidbody2D.AddForce(forceDirection, ForceMode2D.Force);
                //this.rigidbody.AddForce(forceDirection, ForceMode.Force);
            }
            else
            {
                if (walk)
                {
                    this.transform.Translate(Vector3.down * speed * Time.deltaTime);
                    if (fromStartToEnd)
                    {
                        //if (this.transform.position.x >= EndPoint.transform.position.x)
                        if (Vector3.Magnitude(this.transform.position - EndPoint.transform.position) <= 0.3f)
                        {
                            this.transform.Rotate(Vector3.forward, 180);
                            fromStartToEnd = false;
                        }
                    }
                    else
                    {
                        if (Vector3.Magnitude(this.transform.position - StartPoint.transform.position) <= 0.3f)
                        {
                            this.transform.Rotate(Vector3.forward, 180);
                            fromStartToEnd = true;
                        }
                    }
                }
                else
                {
                    if (target != null)
                    {
                        Quaternion newRotation = Quaternion.LookRotation(target.transform.position - this.transform.position, Vector3.forward);
                        transform.rotation = newRotation;
                        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
                        Shoot();
                    }
                    if (startCounting)
                    {
                        timer += Time.deltaTime;
                        if (timer >= Cooldown)
                        {
                            timer = 0.0f;
                            canShoot = true;
                            startCounting = false;
                        }
                    }
                }
            }
        }
        else
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= 0.25f)
            {
                BloodPlane.renderer.enabled = true;
            }
        }
        if(doAlienGunFakeParticle)
        {
            fakeParticleTimer += Time.deltaTime;
            if(fakeParticleTimer >= fakeParticleCooldown)
            {
                AlienGunFakeParticle.renderer.enabled = false;
                fakeParticleTimer = 0.0f;
                doAlienGunFakeParticle = false;
            }
        }
	}

    void StopWalking()
    {
        walk = false;
        anim.enabled = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            StopWalking();
            target = col.gameObject;
        }
    }

    void Shoot()
    {
        if(canShoot)
        {
            canShoot = false;
			audio.PlayOneShot(ShootSound);
            startCounting = true;
            Quaternion bulletRotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
            Instantiate(Bullet, MyArm.transform.position, bulletRotation);
            AlienGunFakeParticle.renderer.enabled = true;
            doAlienGunFakeParticle = true;
        }
    }

    void Defy(GameObject ForceCenter)
    {
        this.ForceCenter = ForceCenter;
        affected = true;
    }

    void StopDefying()
    {
        affected = false;
    }

    void Die()
    {
        isAlive = false;
        anim.enabled = true;
		AlienGunFakeParticle.renderer.enabled = false;
        anim.Play("death");
        if (this.rigidbody2D == null)
        {
            this.gameObject.AddComponent<Rigidbody2D>();
        }
        this.rigidbody2D.velocity = new Vector2(0, 0);
        Vector3 pos = this.transform.position;
        pos.z = 5;
        this.transform.position = pos;
        //this.collider2D.enabled = false;
    }
}
