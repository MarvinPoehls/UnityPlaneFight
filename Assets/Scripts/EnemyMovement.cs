using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : PlaneMovement
{
    [SerializeField] EnemyStatus enemyStatus;
    [SerializeField] protected float shootRange;
    protected Transform playerTransform;

    [SerializeField] protected EnemyHealthBar healthBar;

    public enum EnemyStatus
    {
        Searching,
        Chasing,
        Fleeing
    }

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;

    }

    protected override void Update()
    {
        base.Update();

        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    protected void FixedUpdate()
    {
        if (!isDead)
        {
            switch (enemyStatus)
            {
                case EnemyStatus.Searching:
                    SearchPlayer();
                    break;
                case EnemyStatus.Chasing:
                    ChasePlayer();
                    break;
                case EnemyStatus.Fleeing:
                    Flee();
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Health -= collision.gameObject.GetComponent<Bullet>().GetDamage();
            healthBar.UpdateHealth(Health);
        }
    }

    protected void ChasePlayer()
    {
        RotateToPlayer();

        MoveToPlayerHeight();

        if (IsInShootingRange())
        {
            if (rb.velocity.magnitude > 7 || transform.position.y > playerTransform.position.y + 20)
            {
                Brake();
            }

            Shoot();
        }

        MoveForward();
    }

    protected void SearchPlayer()
    {

    }

    protected void Flee()
    {

    }

    protected void RotateToPlayer()
    {
        Vector3 current = transform.right;
        Vector3 to = playerTransform.position - transform.position;
        transform.right = Vector3.RotateTowards(current, to, RotationControl * Time.deltaTime, 10);
    }

    protected bool IsInShootingRange()
    {
        bool inRange = Vector2.Distance(transform.position, playerTransform.position) < shootRange;
        return inRange;
    }

    protected void MoveForward()
    {
        Vector2 velocity = transform.right * Acceleration;
        velocity += Vector2.up * lift;

        rb.AddForce(velocity);
    }

    protected void MoveToPlayerHeight()
    {
        float planeY = transform.position.y;
        float playerPlaneY = playerTransform.position.y;


        if (planeY > playerPlaneY + 50 && planeY > 0)
        {
            Brake();
        }
        if (planeY > playerPlaneY + 10)
        {
            //rotate down
        } 
        if (planeY < playerPlaneY - 10)
        {
            //rotate up
        }
    }
}
