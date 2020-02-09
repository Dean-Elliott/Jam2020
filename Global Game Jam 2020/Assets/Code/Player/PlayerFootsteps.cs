using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem footsteps;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        ParticleSystem.EmissionModule emission = footsteps.emission;
        emission.enabled = playerMovement.IsGrounded && playerMovement.IsMoving;
    }
}
