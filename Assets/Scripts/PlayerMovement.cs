using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : PlaneMovement
{
    [SerializeField] Hud hud;

    protected void Awake()
    {
        hud.SetMaxCooldown(shootObjects[selectedWeapon].GetComponent<Projectile>().GetCoolDown());
    }

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

        SetSelectedWeapon();
        hud.UpdateStamina(Stamina);
        hud.UpdateCooldown(GetActiveCoolDown());
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
            Health -= collision.gameObject.GetComponent<Projectile>().GetDamage();
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

    protected void SetSelectedWeapon()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
        }
    }

    protected void SwitchWeapon(int selectedWeapon)
    {
        this.selectedWeapon = selectedWeapon;
        hud.SetWeaponSprite(shootObjects[selectedWeapon].GetComponent<SpriteRenderer>().sprite);
        hud.SetMaxCooldown(shootObjects[selectedWeapon].GetComponent<Projectile>().GetCoolDown());
        ResetCoolDown();
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
