using UnityEngine;
using System.Collections;

public class AIvertical : MonoBehaviour {

    public GameObject StartPoint;
    public GameObject EndPoint;
    public float speed = 5.0f;
    public GameObject Bullet;
    public float Cooldown;
    public GameObject MyArm;
	public AudioClip ShootSound;
    public GameObject BloodPlane;

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

	// Use this for initialization
	void Start () {
        this.transform.position = StartPoint.transform.position;
        anim = GetComponent<Animator>();
        if(StartPoint.transform.position == EndPoint.transform.position)
        {
            walk = false;
        }
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
                        if (this.transform.position.y >= EndPoint.transform.position.y)
                        //if (Vector3.Magnitude(this.transform.position - EndPoint.transform.position) <= 0.3f)
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
                        newRotation.z += 90;
                        if (!fromStartToEnd)
                        {
                            newRotation.y += 180;
                        }
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
        anim.Play("death");
        this.collider2D.enabled = false;
        this.transform.Translate(0, 0, 2);
    }
}
