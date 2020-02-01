using UnityEngine;

public class PlayerHammerHead : MonoBehaviour
{
    public PlayerHammer Hammer { get; private set; }

    private float nextNailIn;

    private void Awake()
    {
        Hammer = GetComponentInParent<PlayerHammer>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //too soon
        if (Time.time < nextNailIn)
        {
            return;
        }

        if (Hammer.IsHammering)
        {
            Nail nail = collision.GetComponentInParent<Nail>();
            if (nail)
            {
                nextNailIn = Time.time + 0.25f;
                nail.NailMeIn();
            }
        }
    }
}