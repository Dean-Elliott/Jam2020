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

    public Vector3 PaintTipPosition
    {
        get
        {
            Vector3 pos = transform.position + new Vector3(RightStick.x, 0f, RightStick.y).normalized * 1.8f;
            return pos;
        }
    }

    /// <summary>
    /// The colour of the paint brush.
    /// </summary>
    public ColorType Color { get; set; } = ColorType.None;

    protected override void OnUpdate()
    {
        PaintBucket[] buckets = FindObjectsOfType<PaintBucket>();
        foreach (PaintBucket bucket in buckets)
        {
            if (bucket.Bounds.Contains(PaintTipPosition))
            {
                Color = bucket.Color;
            }
        }

        paintMaterial.color = Paintable.GetColor(Color);

        Vector2 rightStick = RightStick;

        if (Trigger > 0.1f && rightStick.sqrMagnitude > 0.1f)
        {
            Paint();
        }

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
        if (rightStick.sqrMagnitude > 0.02f)
        {
            Vector3 eulerAngles = new Vector3(magnitude * 90f, x, 0f);
            transform.eulerAngles = eulerAngles;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Paintable.GetColor(Color);
        Gizmos.DrawWireSphere(PaintTipPosition, paintBrushRadius);
    }

    private void Paint()
    {
        Paintable[] paintables = FindObjectsOfType<Paintable>();
        float closestDistance = float.MaxValue;
        Paintable closestPaintable = null;
        foreach (Paintable paintable in paintables)
        {
            float dist = (paintable.transform.position - PaintTipPosition).sqrMagnitude;
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
