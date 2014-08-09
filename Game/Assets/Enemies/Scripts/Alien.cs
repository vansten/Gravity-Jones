using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DetectBehaviour {Shoot, Chase};

public class Alien : MonoBehaviour {

    public float MovementSpeed = 25.0f;
    public float ForcePower = 75.0f;
    public DetectBehaviour DetectAction = DetectBehaviour.Shoot;
    public float ShootCooldown = 0.7f;
    public GameObject Bullet;
    public GameObject Arm;
    public GameObject Blood;
    public Animator anim;
    public new AudioSource[] audio;
    public AudioClip WalkSound;
    public AudioClip ShootSound;
    public AudioClip DeathSound;
    public List<GameObject> Nodes;
    public bool Circle = false;
    public bool StayInPosition = false;

    private bool affected = false;
    private bool playerDetected = false;
    private GameObject player;
    private bool canShoot = true;
    private float timer = 0.0f;
    private bool dead = false;
    private float deathTimer = 0.0f;
    private bool stayInPosition = false;
    private int currentNode = 0;
    private int nextNode = 1;
    private bool fromStartToEnd = true;

	// Use this for initialization
	void Start () {
        int min = Mathf.Min(Nodes.Count, 2);
	    if(min < 2 || StayInPosition)
        {
            stayInPosition = true;
        }
        else
        {
            stayInPosition = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(dead)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer > 0.4f)
            {
                Blood.renderer.enabled = true;
            }
        }
        else
        {
            if (!affected)
            {
                if (playerDetected)
                {
                    ChooseAction();
                }
                else
                {
                    canShoot = true;
                    timer = 0.0f;
                    if (!stayInPosition)
                    {
                        Patrol();
                    }
                }
            }
        }
	}

    void ChooseAction()
    {
        switch (DetectAction)
        {
            case DetectBehaviour.Chase:
                Chase();
                break;
            case DetectBehaviour.Shoot:
                Shoot();
                break;
        }
    }

    void Shoot()
    {
        this.transform.up = -(player.transform.position - this.transform.position).normalized;
        if(canShoot)
        {
            Instantiate(Bullet, Arm.transform.position, this.transform.rotation);
            audio[1].PlayOneShot(ShootSound);
            canShoot = false;
        }
        else
        {
            timer += Time.deltaTime;
            if(timer > ShootCooldown)
            {
                canShoot = true;
                timer = 0.0f;
            }
        }
    }

    void Chase()
    {
        //To do another time
        Debug.Log("I'm chasing");
    }

    void Patrol()
    {
        Vector2 direction = -(Nodes[nextNode].transform.position - this.transform.position);
        this.transform.up = direction.normalized;
        int dir = 1;
        if (!fromStartToEnd)
        {
            dir = -1;
        }
        this.transform.Translate(this.transform.up * MovementSpeed * dir * Time.deltaTime);
        if(direction.magnitude < 2.0f)
        {
            currentNode = nextNode;
            nextNode = getNextNode();
        }
    }

    void Die(string cause)
    {
        if(!dead)
        {
            dead = true;
            anim.Play("death");
            audio[2].PlayOneShot(DeathSound);
        }
    }

    void Defy(GameObject ForceCenter)
    {
        Vector2 direction = (this.transform.position - ForceCenter.transform.position).normalized;
        float forcePower = ForcePower / direction.magnitude;
        this.rigidbody2D.AddForce(direction * forcePower);
        affected = true;
    }

    void StopDefying()
    {
        affected = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerDetected = true;
            player = col.gameObject;
        }
    }

    int getNextNode()
    {
        if(Circle)
        {
            if(currentNode + 1 >= Nodes.Count)
            {
                return 0;
            }
            else
            {
                return currentNode + 1;
            }
        }
        else
        {
            if (fromStartToEnd)
            {
                if (currentNode + 1 >= Nodes.Count)
                {
                    fromStartToEnd = false;
                    return currentNode - 1;
                }
                else
                {
                    return currentNode + 1;
                }
            }
            else
            {
                if (currentNode - 1 < 0)
                {
                    fromStartToEnd = true;
                    return currentNode + 1;
                }
                else
                {
                    return currentNode - 1;
                }
            }
        }
    }
}
