using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyMovement;
using static EnemyPointerWindow;

public class EnemyHelicopterMovement : HelicopterMovement
{
    protected Transform playerTransform;

    [SerializeField] protected EnemyHealthBar healthBar;

    protected EnemyPointerWindow enemyPointerWindow;
    protected EnemyPointer enemyPointer;

    [SerializeField] protected float shootRange;

    private void FixedUpdate()
    {
        if (isDead)
        {
            enemyPointerWindow.DestroyPointer(enemyPointer);
        }

        if (!isDead && playerTransform)
        {
            Vector3 playerDirection = (playerTransform.position - transform.position).normalized;

            Move(playerDirection);

            if (IsInShootingRange())
            {
                Shoot(playerDirection);
            }
        }
        
    }

    protected override void Awake()
    {
        base.Awake();

        playerTransform = GameObject.FindWithTag("Player").transform;

        healthBar.SetMaxHealth(Health);

        enemyPointerWindow = FindObjectOfType<EnemyPointerWindow>();
        enemyPointer = enemyPointerWindow.CreatePointer(transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Health -= collision.gameObject.GetComponent<Bullet>().GetDamage();
            healthBar.UpdateHealth(Health);
        }
    }

    protected bool IsInShootingRange()
    {
        bool inRange = Vector2.Distance(transform.position, playerTransform.position) < shootRange;
        return inRange;
    }
}