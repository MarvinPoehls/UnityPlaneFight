using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public float MaxSpeed;
    public float Acceleration;

    protected Rigidbody2D rb;

    public float RotationControl;

    protected float MovY = 1;
    protected float lift = 9.81f;

    public GameObject shootObject;
    public Transform bullletSpawn;

    protected float timeStamp;
    public float coolDownInSeconds;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        MovY = Input.GetAxis("Vertical");
    }

    protected void Shoot()
    {
        if (timeStamp <= Time.time)
        {
            Instantiate(shootObject, bullletSpawn.position, transform.rotation);
            timeStamp = Time.time + coolDownInSeconds;
        }
    }

    protected void Brake()
    {
        if (rb.velocity.x > 0)
        {
            rb.AddForce(transform.right * -10);
        }
    }

    protected void SetRotation()
    {
        float Dir = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.right));

        if(Acceleration > 0)
        {
            if(Dir > 0)
            {
                rb.rotation += MovY * RotationControl * (rb.velocity.magnitude / MaxSpeed); 
            }
            else
            {
                rb.rotation -= MovY * RotationControl * (rb.velocity.magnitude / MaxSpeed); 
            }
        }
    }

    protected void SpeedLimit()
    {
        if(rb.velocity.magnitude > MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        }
    }

    protected void FilpPlane()
    {
        //TODO transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
    }

    protected bool IsPlaneUpsideDown()
    {
        return transform.up.y < 0f;
    }
}