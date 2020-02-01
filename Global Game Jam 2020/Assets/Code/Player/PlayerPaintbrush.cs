using System;
using UnityEngine;

public class PlayerPaintbrush : Player
{
    [SerializeField]
    private Renderer[] renderers = { };

    [SerializeField]
    private bool invert = false;

    /// <summary>
    /// The colour of the paint brush.
    /// </summary>
    public Color Color { get; set; } = Color.white;

    protected override void OnUpdate()
    {
        PaintBucket[] buckets = FindObjectsOfType<PaintBucket>();
        foreach (PaintBucket bucket in buckets)
        {
            if (bucket.Bounds.Contains(transform.position))
            {
                Color = bucket.Color;
            }
        }

        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = Color;
        }

        if (LeftTrigger > 0.2f)
        {
            Paint();
        }

        Vector2 rightStick = RightStick;
        if (invert)
        {
            rightStick *= -1f;
        }

        float x = rightStick.x;
        float magnitude = rightStick.magnitude;
        if (rightStick.y < 0f)
        {
            magnitude *= -1f;
            x *= -1f;
        }

        Vector3 eulerAngles = new Vector3(magnitude * 90f, x * 90f, 0f);
        transform.eulerAngles = eulerAngles;
    }

    private void Paint()
    {

    }
}
