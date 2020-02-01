using UnityEngine;

public class PaintBucket : MonoBehaviour
{
    [SerializeField]
    private Vector3 size = new Vector3(1f, 1f, 1f);

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0f, 0f);

    [SerializeField]
    private ColorType color = ColorType.Red;

    [SerializeField]
    private Renderer[] renderers = { };

    public Bounds Bounds => new Bounds(transform.position + offset, size);
    public ColorType Color => color;

    private void OnDrawGizmos()
    {
        Gizmos.color = Paintable.GetColor(color);
        Gizmos.DrawWireCube(Bounds.center, Bounds.size);
    }

    private void Update()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = Paintable.GetColor(color);
        }
    }
}