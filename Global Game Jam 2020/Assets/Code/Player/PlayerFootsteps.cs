using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem footsteps;

    [SerializeField]
    private ParticleSystem footsteps2;


    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        ParticleSystem.EmissionModule emission = footsteps.emission;
        ParticleSystem.EmissionModule emission2 = footsteps2.emission;
        emission.enabled = playerMovement.IsGrounded && playerMovement.IsMoving;
    }
}
