using UnityEngine;

public class Nail : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int nailInStage = 0;

    [SerializeField]
    private int maxNailInStage = 2;

    [SerializeField]
    private float nailedInDistance = 0;

    private Vector3 originalPosition;

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
        originalPosition = transform.localPosition;
    }

    public void NailMeIn()
    {
        nailInStage++;
        if (nailInStage > maxNailInStage)
        {
            nailInStage = maxNailInStage;
        }
    }

    private void Update()
    {
        Vector3 nailedInPosition = originalPosition - transform.up * nailedInDistance;
        transform.localPosition = Vector3.Lerp(originalPosition, nailedInPosition, Percentage);
    }
}