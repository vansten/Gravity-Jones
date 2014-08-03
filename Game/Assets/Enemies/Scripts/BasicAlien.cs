using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicAlien : AIbase {

    //Update function
    void Update()
    {
        if (isAlive)
        {
            if (!stayInPosition)
            {
                if(walk)
                {
                    timer = 0.0f;
                    startCounting = false;
                    this.Move();
                }
                else
                {
                    this.Track();
                }
            }
            else if(target != null)
            {
                this.Track();
            }
        }
        else
        {
            deathTimer += Time.deltaTime;
            if(deathTimer > 0.4f)
            {
                BloodPlane.renderer.enabled = true;
            }
        }
    }

    public override void Track()
    {
        if(target == null)
        {
            return;
        }
        transform.up = (this.transform.position - target.transform.position).normalized;
        this.Shoot();
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
