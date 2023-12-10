using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMovement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float Health;
    [SerializeField] protected float acceleration;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected Animator propellerAnimator;

    [SerializeField] private GameObject shootObject;
    [SerializeField] private Transform bullletSpawn;
    protected float timeStamp;
    [SerializeField] protected float coolDownInSeconds;
    private Quaternion gunRotation;

    protected bool isDead = false;

    protected virtual void Awake()
    {
        RotateGunInDirection(Vector2.right);
    }

    protected void Update()
    {
        Flip(rb.velocity.x);

        SpeedLimit();

        if (Health <= 0)
        {
            isDead = true;
        }

        if (isDead)
        {
            propellerAnimator.enabled = false;
            rb.gravityScale = 2;
            Destroy(gameObject, 2);
        }
    }

    protected void Move(Vector3 direction)
    {
        rb.AddForce(direction.normalized * acceleration);
    }

    protected void Shoot(Vector3 direction)
    {
        if (timeStamp <= Time.time)
        {
            Instantiate(shootObject, bullletSpawn.position, gunRotation);
            timeStamp = Time.time + coolDownInSeconds;

            RotateGunInDirection(direction);
        }
    }

    protected void Flip(float movingDirection)
    {
        if (movingDirection < 0)
        {
            transform.localScale = new Vector3(-3, transform.localScale.y, transform.localScale.z);
        }
        if (movingDirection > 0)
        {
            transform.localScale = new Vector3(3, transform.localScale.y, transform.localScale.z);
        }
    }

    protected void SpeedLimit()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void RotateGunInDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunRotation = Quaternion.Euler(0f, 0f, angle);
    }
}