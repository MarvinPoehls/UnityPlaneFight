using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : PlaneMovement
{
    private EnemyStatus enemyStatus;

    public enum EnemyStatus
    {
        Searching,
        Chasing,
        Fleeing
    }

    private void FixedUpdate()
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

    private void ChasePlayer()
    {
        //RotateToPlayer()
        //if(player in front of me)
        //{
        //Shoot()
        //}
        //MoveForward()
    }

    private void SearchPlayer()
    {

    }

    private void Flee()
    {

    }
}
