using UnityEngine;

public class PlayerHammerHead : MonoBehaviour
{
    public PlayerHammer Hammer { get; private set; }

    public AudioClip hammerHit;
    AudioSource audioSource;

    private float nextNailIn;

    private void Awake()
    {
        Hammer = GetComponentInParent<PlayerHammer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //too soon
        if (Time.time < nextNailIn)
        {
            audioSource.PlayOneShot(hammerHit, 0.7F);
            return;
        }

        if (Hammer.IsHammering)
        {
            Nail nail = collision.GetComponentInParent<Nail>();
            if (nail)
            {
                audioSource.PlayOneShot(hammerHit, 0.7F);
                nextNailIn = Time.time + 0.25f;
                nail.NailMeIn();
            }
        }
    }
}