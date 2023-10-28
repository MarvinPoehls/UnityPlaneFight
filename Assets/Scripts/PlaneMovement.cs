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

        if(Input.GetKey(KeyCode.LeftArrow)){
            Brake();
        }
    }

    private void MoveForward()
    {
        lift = 9.81f;
        if (Input.GetKey(KeyCode.LeftArrow))
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
}