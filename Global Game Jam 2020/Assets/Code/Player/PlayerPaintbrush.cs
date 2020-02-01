using System;
using UnityEngine;

public enum ColorType
{
    Red,
    Yellow,
    Blue,
    None
}

public class PlayerPaintbrush : Player
{
    [SerializeField]
    private float paintBrushRadius = 1.2f;

    [SerializeField]
    private Material paintMaterial;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private PaintBall paintSpill;

    [SerializeField]
    private Transform paintSpawnpoint;

    /// <summary>
    /// The colour of the paint brush.
    /// </summary>
    public ColorType Color { get; set; } = ColorType.None;

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

        paintMaterial.color = Paintable.GetColor(Color);

        if (LeftTrigger > 0.1f)
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

        x *= 90f;
        Vector3 eulerAngles = new Vector3(magnitude * 90f, x, 0f);
        transform.eulerAngles = eulerAngles;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Paintable.GetColor(Color);
        Gizmos.DrawWireSphere(paintSpawnpoint.position, paintBrushRadius);
    }

    private void Paint()
    {
        Paintable[] paintables = FindObjectsOfType<Paintable>();
        float closestDistance = float.MaxValue;
        Paintable closestPaintable = null;
        foreach (Paintable paintable in paintables)
        {
            float dist = (paintable.transform.position - paintSpawnpoint.position).sqrMagnitude;
            if (dist < closestDistance && dist < paintBrushRadius * paintBrushRadius)
            {
                closestDistance = dist;
                closestPaintable = paintable;
            }
        }

        //paint the thingy
        if (closestPaintable)
        {
            closestPaintable.Paint(Color);
        }
    }
}
