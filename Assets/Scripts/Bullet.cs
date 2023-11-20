using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] private float damage;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void FixedUpdate()
    {
        transform.position += bulletSpeed * Time.deltaTime * transform.right;
    }

    public float GetDamage()
    {
        return damage;
    }
}
