using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    protected Transform targetTransform;
    [SerializeField] protected string targetTag;
    [SerializeField] protected float rotationControl;
    protected Animator animator;
    protected bool isDestroyed;

    private void Awake()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        targetTransform = GetNearestTarget(targets).transform;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isDestroyed)
        {
            if (targetTransform)
            {
                RotateToTarget();
            }
            transform.position += speed * Time.deltaTime * transform.right;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
    }

    protected void Explode()
    {
        transform.localScale = Vector3.one * 5;
        isDestroyed = true;
        animator.Play("Rocket_explosion");
        Destroy(gameObject, 0.5f);
    }

    protected GameObject GetNearestTarget(GameObject[] targets)
    {
        if (targets.Length == 1)
        {
            return targets[0];
        }
        return targets[0];
    }

    protected void RotateToTarget()
    {
        Vector3 current = transform.right;
        Vector3 to = targetTransform.position - transform.position;
        transform.right = Vector3.RotateTowards(current, to, rotationControl * Time.deltaTime, 10);
    }
}
