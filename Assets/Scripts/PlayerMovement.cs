using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : PlaneMovement
{
    [SerializeField] HealthBar hud;

    protected override void Update()
    {
        base.Update();

        if (IsBoostInput())
        {
            Boost();
        }

        if (IsBoostEndInput())
        {
            EndBoost();
        }

        hud.UpdateStamina(Stamina);
    }

    protected void FixedUpdate()
    {
        if (!isDead)
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Health -= collision.gameObject.GetComponent<Bullet>().GetDamage();
            hud.UpdateHealth(Health);
        }
    }

    protected void MoveForward()
    {
        if (IsBrakeInput())
        {
            lift = 0;
        }

        Vector2 velocity = transform.right * Acceleration;
        velocity += Vector2.up * lift;

        rb.AddForce(velocity);
    }

    protected bool IsBrakeInput()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    protected bool IsShootInput()
    {
        return Input.GetKey(KeyCode.Space);
    }

    protected bool IsBoostInput()
    {
        return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
    }

    protected bool IsBoostEndInput()
    {
        return Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D);
    }
}
