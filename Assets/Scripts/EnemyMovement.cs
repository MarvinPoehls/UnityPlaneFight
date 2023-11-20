using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : PlaneMovement
{
    [SerializeField] EnemyStatus enemyStatus;
    [SerializeField] Transform playerTransform;

    Transform entity;
    Vector3 object_position;
    [Range(2, 30)]
    float angle_range = 30f;
    [Range(0.5f, 5f)]
    float min_allowed_distance = 3f;

    public enum EnemyStatus
    {
        Searching,
        Chasing,
        Fleeing
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

    protected void ChasePlayer()
    {
        RotateToPlayer();

        if(true)
        {
            Shoot();
        }

        MoveForward();

        if (IsPlayerBehind())
        {
            Brake();
        } 
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

    protected void MoveForward()
    {
        Vector2 velocity = transform.right * Acceleration;
        velocity += Vector2.up * lift;

        rb.AddForce(velocity);
    }

    protected bool IsPlayerBehind()
    {
        return false;
        //Just assign these references however you need to. Either at run time, or in the inspector, or whatever suits your needs.
        //Use an angle_range of 2 - 30 for best results. A value of 2 being directly in front of the entity. Increasing this value
        //increases the entity's peripheral vision. Using 0 is not recommended. The value will most likely never equal 0.

        float angle = Vector3.Angle(entity.forward, object_position - entity.position);

        if (Mathf.Abs(angle) < angle_range)
        {
            //return distance < min_allowed_distance;
        }
    }
}
