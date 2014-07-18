using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicAlien : AIbase {

    private bool rotated = false;

    //Update function
    void Update()
    {
        if (isAlive)
        {
            if (!stayInPosition)
            {
                if(walk)
                {
                    rotated = false;
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
        if (!rotated)
        {
            float angle = Vector2.Angle(target.transform.position, this.transform.position);
            this.transform.Rotate(Vector3.forward, angle);
            rotated = true;
        }
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
