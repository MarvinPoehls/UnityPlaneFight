using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlaneMovement
{
    private void FixedUpdate()
    {
        MoveForward();

        SetRotation();

        float thrustforce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.down)) * 2.0f;

        Vector2 relForce = Vector2.up * thrustforce;

        rb.AddForce(rb.GetRelativeVector(relForce));

        SpeedLimit();

        if (IsBrakeInput())
        {
            Brake();
        }

        if (IsShootInput())
        {
            Shoot();
        }

        if (IsPlaneUpsideDown())
        {
            FilpPlane();
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

    private bool IsBrakeInput()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    private bool IsShootInput()
    {
        return Input.GetKey(KeyCode.Space);
    }
}
