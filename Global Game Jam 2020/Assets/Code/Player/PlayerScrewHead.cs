using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScrewHead : MonoBehaviour
{
    public PlayerScrewdriver ScrewDriver { get; private set; }

    public AudioClip screwIn;
    AudioSource audioSource;
    private float screwingTime;
    public GameObject screwEffect;

    private void Awake()
    {
        ScrewDriver = GetComponentInParent<PlayerScrewdriver>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //too soon
        if (Time.time < screwingTime)
        {
            if (audioSource)
            {
                //audioSource.PlayOneShot(hammerHit, 0.7F);
            }

            return;
        }

        if (!ScrewDriver)
        {
            ScrewDriver = GetComponentInParent<PlayerScrewdriver>();
        }

        if (ScrewDriver && ScrewDriver.IsScrewingIn)
        {
            Screw screw = collision.GetComponentInParent<Screw>();
            if (screw)
            {
                if (audioSource)
                {
                   // audioSource.PlayOneShot(hammerHit, 0.7F);
                }

               // screwingTime = Time.time + 0.25f;
                
              //  screw.screwingTime();

                if (screwEffect)
                {
                    var newEffect = Instantiate(screwEffect, transform.position, Quaternion.identity);
                   // Destroy(newEffect, 2f);
                }
            }
        }
    }
}