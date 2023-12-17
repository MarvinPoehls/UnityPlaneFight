using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float coolDown;
    [SerializeField] protected float aliveTimeInSeconds;

    protected void Start()
    {
        Destroy(gameObject, aliveTimeInSeconds);
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetCoolDown() 
    {
        return coolDown; 
    }
}