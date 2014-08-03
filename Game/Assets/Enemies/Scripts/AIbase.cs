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
    protected bool stayInPosition = false;
    protected bool fromStartToEnd = true;
    private int currentNode = 0;
    private int nextNode = 1;

	// Use this for initialization
    public void Start()
    {
        anim = this.GetComponent<Animator>();
        if(Nodes.Count < 2)
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
        }

        if (!stayInPosition)
        {
            nextNode = 1;
            this.transform.position = Nodes[0].position;
            transform.up = (this.transform.position - Nodes[nextNode].position).normalized;
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
            Instantiate(Bullet, MyArm.transform.position, this.transform.rotation);
        }
    }

    public void Defy(GameObject ForceCenter)
    {
        this.ForceCenter = ForceCenter;
        affected = true;
        Affect();
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
            audio.PlayOneShot(DeathSound);
            anim.Play("death");
            if (this.rigidbody2D == null)
            {
                this.gameObject.AddComponent<Rigidbody2D>();
            }
            this.rigidbody2D.velocity = new Vector2(0, 0);
            this.transform.Translate(0, 0, 5);
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
        this.transform.Translate(-Vector2.up * this.speed * Time.deltaTime);
        float distanceToNextNode = (this.transform.position - Nodes[nextNode].position).magnitude;
        if(distanceToNextNode < 0.3f)
        {
            Rotate();
        }
    }

    public virtual void Track()
    {

    }

    int getNextNode()
    {
       int val = nextNode;
       if(Circle)
       {
           val += 1;
           if(val == Nodes.Count)
           {
               val = 0;
           }
       }
       else
       {
           if(fromStartToEnd)
           {
               val += 1;
               if(val == Nodes.Count)
               {
                   val -= 2;
                   fromStartToEnd = false;
               }
           }
           else
           {
               val -= 1;
               if(val == -1)
               {
                   val = 1;
                   fromStartToEnd = true;
               }
           }
       }

       return val;
    }

    void Rotate()
    {
        transform.up = (Nodes[nextNode].position - Nodes[currentNode].position).normalized;
        currentNode = nextNode;
        nextNode = getNextNode();
        Debug.Log(transform.up);
    }
}
