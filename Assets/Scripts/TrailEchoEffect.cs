using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEchoEffect : MonoBehaviour
{
    [SerializeField] float timeBtwSpawns;
    [SerializeField] float startTimeBtwSpawns;
    [SerializeField] GameObject echo;

    private void Update()
    {
        PlayerMovement player = GetComponentInParent<PlayerMovement>();

        if (!player.IsDead())
        {
            if (timeBtwSpawns <= 0 && IsBraking() == false)
            {
                Instantiate(echo, transform.position, transform.parent.transform.rotation);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }

    private bool IsBraking()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }
}
