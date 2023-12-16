using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    private void FixedUpdate()
    {
        transform.position += speed * Time.deltaTime * transform.right;
    }
}
