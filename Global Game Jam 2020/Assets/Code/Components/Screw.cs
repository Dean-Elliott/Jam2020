using UnityEngine;

public class Screw : MonoBehaviour, IInteractable
{
    [SerializeField, Range(0f, 1f)]
    public float progress = 0f;

    [SerializeField]
    private float screwInSpeed = 1f;

    [SerializeField]
    private float radius = 0.5f;

    [SerializeField]
    private float screwedInDistance = 1f;

    [SerializeField]
    private Transform top;

    [SerializeField]
    private Transform screwVisual;

    [SerializeField]
    private AnimationCurve screwCurve = new AnimationCurve();

    private Vector3 originalPosition;
    private Vector3 destinationPosition;

    /// <summary>
    /// The rotation of the screw on its local Y axis.
    /// </summary>
    public float Rotation { get; set; }

    /// <summary>
    /// World position of the top of the screw.
    /// </summary>
    public Vector3 Top
    {
        get
        {
            return transform.position - transform.up * screwedInDistance * Percentage;
        }
    }

    public Vector3 OriginalTop
    {
        get
        {
            return transform.TransformPoint(originalPosition);
        }
    }

    public float Percentage => progress;
    public Bounds Bounds => new Bounds(transform.position - transform.up * screwedInDistance * 0.5f, new Vector3(radius, screwedInDistance, radius));

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * screwedInDistance);
        Gizmos.DrawWireSphere(transform.position - transform.up * screwedInDistance, 0.25f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position - transform.up * screwedInDistance * Percentage, 0.25f);

        Gizmos.DrawWireCube(transform.position - transform.up * screwedInDistance * 0.5f, new Vector3(radius, screwedInDistance, radius));
    }

    private void Awake()
    {
        originalPosition = screwVisual.localPosition;
        destinationPosition = screwVisual.localPosition + Vector3.down * screwedInDistance;
    }

    public void ScrewIn(float amount)
    {
        progress += amount * screwInSpeed * Time.deltaTime;
        if (progress > 1f)
        {
            progress = 1f;
        }
    }

    private void Update()
    {
        screwVisual.localPosition = Vector3.Lerp(originalPosition, destinationPosition, screwCurve.Evaluate(Percentage));
        screwVisual.localEulerAngles = new Vector3(-90f, 0.0f, Rotation);
    }
}
