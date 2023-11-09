using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MaxSpeed;
    public float Acceleration;

    Rigidbody2D rb;

    public float RotationControl;

    float MovY = 1;
    float lift = 9.81f;

    public GameObject shootObject;
    public Transform bullletSpawn;

    private float timeStamp;
    public float coolDownInSeconds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovY = Input.GetAxis("Vertical");
    }

   
    private void FixedUpdate()
    {
        MoveForward();

        SetRotation();

        float thrustforce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.down)) * 2.0f;

        Vector2 relForce = Vector2.up * thrustforce;

        rb.AddForce(rb.GetRelativeVector(relForce));

        SpeedLimit();

        if(IsBrakeInput())
        {
            Brake();
        }

        if(IsShootInput()) { 
            Shoot(); 
        }

        if(IsPlaneUpsideDown())
        {
            FilpPlane();
        }
    }

    private void Shoot()
    {
        if (timeStamp <= Time.time)
        {
            Instantiate(shootObject, bullletSpawn.position, transform.rotation);
            timeStamp = Time.time + coolDownInSeconds;
        }
    }

    private void MoveForward()
    {
        lift = 9.81f;
        if (IsBrakeInput())
        {
            lift = 0;
        }

        Vector2 velocity = transform.right * Acceleration;
        velocity += Vector2.up * lift;

        rb.AddForce(velocity);
    }

    private void Brake()
    {
        if (rb.velocity.x > 0)
        {
            rb.AddForce(transform.right * -10);
        }
    }

    private void SetRotation()
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

    private void SpeedLimit()
    {
        if(rb.velocity.magnitude > MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        }
    }

    private bool IsBrakeInput()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    private bool IsShootInput()
    {
        return Input.GetKey(KeyCode.Space);
    }

    private void FilpPlane()
    {
        //TODO transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
    }

    private bool IsPlaneUpsideDown()
    {
        return transform.up.y < 0f;
    }
}