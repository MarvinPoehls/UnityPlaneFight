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
        HandleMovement();

        SetRotation();

        float thrustforce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.down)) * 2.0f;

        Vector2 relForce = Vector2.up * thrustforce;

        rb.AddForce(rb.GetRelativeVector(relForce));

        SpeedLimit();

        if(Input.GetKey(KeyCode.LeftArrow)){
            Brake();
        }
    }

    private void HandleMovement()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.gravityScale = 0;
        }
        
        if(Input.GetKey(KeyCode.Space)){
            MoveForward();
        }

        if(Input.GetKey(KeyCode.Space)){
            rb.gravityScale = 0;
        }
    }

    private void MoveForward()
    {
        Vector2 velocity = transform.right * Acceleration;
        rb.AddForce(velocity);
    }

    private void Brake()
    {
        rb.velocity *= 0.9f;
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