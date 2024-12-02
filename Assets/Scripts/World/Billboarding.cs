using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Billboarding : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 dir = new Vector3(0, 0, 0);
    private bool canSpin = true;

    [SerializeField] private float minPlayerDistance = 2f;

    void Start()
    {
        FirstPersonMovement player = FindObjectOfType<FirstPersonMovement>(); //Find the player to look at

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No player found");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            //Find the position
            dir = playerTransform.position - transform.position;
            dir.y = 0;

            if (canSpin == true)
            {
                //Set the rotation of this object to dir
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }

        //Stop moving if the player is too close
        if (dir.x < minPlayerDistance && dir.x > -minPlayerDistance)
        {
            canSpin = false;
        }
        else
        {
            canSpin = true;
        }
    }
}
