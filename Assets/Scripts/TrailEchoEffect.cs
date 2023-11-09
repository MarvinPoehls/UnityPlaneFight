using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEchoEffect : MonoBehaviour
{
    public float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public GameObject echo;
    private Transform planeTransform;

    private void Awake()
    {
        planeTransform = transform.parent.transform;
    }

    private void Update()
    {
        if (timeBtwSpawns <= 0 && isBraking() == false)
        {
            Instantiate(echo, transform.position, planeTransform.rotation);
            timeBtwSpawns = startTimeBtwSpawns;
        } else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }

    private bool isBraking()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }
}
