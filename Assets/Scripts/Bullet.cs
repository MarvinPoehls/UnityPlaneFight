using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;

    private void FixedUpdate()
    {
        transform.position += bulletSpeed * Time.deltaTime * transform.right;
    }
}
