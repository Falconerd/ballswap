using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //playerMovement.Swap();
            playerMovement.LerpToMiddle();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            playerMovement.LerpToTarget();
        }
    }
}
