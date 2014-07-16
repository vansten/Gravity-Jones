using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIbase : MonoBehaviour {

    //public variables
    [Header("GENERAL")]
    public float speed = 5.0f;
    public float Cooldown;
    public GameObject Bullet;
    public GameObject MyArm;
    [Tooltip("This is for blood when alien dies")]
    public GameObject BloodPlane;
    public GameObject AlienGunFakeParticle;
    [Space(2)]
    [Header("SOUNDS")]
    public AudioClip DeathSound;
    public AudioClip ShootSound;
    [Space(5)]
    [Header("PATH")]
    [Tooltip("Check if alien has to going in a circle")]
    public bool Circle = false;
    [Tooltip("Check if alien has to stay in one position")]
    public bool StayInPosition = false;
    public List<Transform> Nodes;

    //proteceted variables
    protected bool walk = true;
    protected Animator anim;
    protected GameObject target;
    protected bool canShoot = true;
    protected bool startCounting = false;
    protected float timer = 0.0f;
    protected GameObject ForceCenter;
    protected bool affected = false;
    protected bool isAlive = true;
    protected float deathTimer = 0.0f;
    protected float fakeParticleTimer = 0.0f;
    protected float fakeParticleCooldown = 0.2f;
    protected bool doAlienGunFakeParticle = false;
    protected bool stayInPosition = false;
    protected int currentNode = 0;
    protected int nextNode = 0;
    protected bool fromStartToEnd = true;

	// Use this for initialization
    public void Start()
    {
        anim = this.GetComponent<Animator>();
        if(Nodes.Count == 0)
        {
           stayInPosition = true;
           if(stayInPosition != StayInPosition)
           {
               Debug.LogError("If you want alien to walk give him a path!");
           }
        }
        else
        {
           stayInPosition = StayInPosition;
           this.transform.position = Nodes[currentNode].position;
           nextNode = 1;
        }
	}
	
	// Update is called once per frame
    void Update()
    {
    }

    public void StopWalking()
    {
        walk = false;
        anim.enabled = false;
    }

    public void StartWalking()
    {
        anim.enabled = true;
        anim.Play("walk");
        walk = true;
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StopWalking();
            target = col.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartWalking();
            target = null;
        }
    }

    public void Shoot()
    {
        if (canShoot)
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

    public void Defy(GameObject ForceCenter)
    {
        this.ForceCenter = ForceCenter;
        affected = true;
    }

    public void StopDefying()
    {
        affected = false;
    }

    public void Die(string cause)
    {
        if (isAlive)
        {
            isAlive = false;
            anim.enabled = true;
            AlienGunFakeParticle.renderer.enabled = false;
            audio.PlayOneShot(DeathSound);
            anim.Play("death");
            if (this.rigidbody2D == null)
            {
                this.gameObject.AddComponent<Rigidbody2D>();
            }
            this.rigidbody2D.velocity = new Vector2(0, 0);
            Vector3 pos = this.transform.position;
            pos.z = 5;
            this.transform.position = pos;
            if(cause == "Bullet")
            {
                this.collider2D.enabled = false;
            }
        }
    }

    public void Affect()
    {
        Vector3 tmp = this.transform.position - ForceCenter.transform.position;
        Vector2 forceDirection = new Vector2(tmp.x, tmp.y);
        forceDirection *= 75.0f / forceDirection.magnitude;
        this.rigidbody2D.AddForce(forceDirection, ForceMode2D.Force);
    }

    public virtual void Move()
    {
        this.transform.Translate(Vector3.down * this.speed * Time.deltaTime);
        
    }

    public virtual void Track()
    {

    }

    int getNextNode()
    {
        if(fromStartToEnd)
        {
            if(currentNode + 1 < Nodes.Count)
            {
                return currentNode + 1;
            }
            else
            {
                if (Circle)
                {
                    return 0;
                }
                else
                {
                    fromStartToEnd = false;
                    return currentNode - 1;
                }
            }
        }
        else
        {
            if(currentNode - 1 >= 0)
            {
                return currentNode - 1;
            }
            else
            {
                fromStartToEnd = true;
                return currentNode + 1;
            }
        }
    }
}
