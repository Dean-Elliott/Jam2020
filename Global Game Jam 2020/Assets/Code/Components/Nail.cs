using cakeslice;
using UnityEngine;

public class Nail : MonoBehaviour, IInteractable
{
    [SerializeField]
    public int nailInStage = 0;

    [SerializeField]
    public int maxNailInStage = 2;

    [SerializeField]
    private float nailedInDistance = 1f;

    [SerializeField]
    private Transform visual;

    [SerializeField]
    private AnimationCurve distanceOverProgress = new AnimationCurve();

    private Outline[] outlines = { };
    private bool selected;
    private float nextUnselect;

    public float Percentage => Mathf.Clamp01(nailInStage / (float)maxNailInStage);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * nailedInDistance);
        Gizmos.DrawWireSphere(transform.position - transform.up * nailedInDistance, 0.25f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position - transform.up * nailedInDistance * Percentage, 0.25f);
    }

    private void Awake()
    {
        outlines = GetComponentsInChildren<Outline>();
    }

    public void NailMeIn()
    {
        nailInStage++;
        if (nailInStage > maxNailInStage)
        {
            nailInStage = maxNailInStage;
        }
    }

    /// <summary>
    /// Highlight this nail for a bit.
    /// </summary>
    public void Highlight()
    {
        //if already all the in, can you dont
        if (Percentage >= 1f)
        {
            return;
        }

        selected = true;
        nextUnselect = Time.time + 0.1f;
    }

    private void Update()
    {
        Vector3 nailedInPosition = Vector3.down * nailedInDistance;
        visual.localPosition = Vector3.Lerp(Vector3.zero, nailedInPosition, distanceOverProgress.Evaluate(Percentage));

        if (Time.time > nextUnselect)
        {
            selected = false;
        }

        foreach (Outline outline in outlines)
        {
            outline.enabled = selected;
        }
    }
}