﻿using UnityEngine;

public class Screw : MonoBehaviour, IInteractable
{
    [SerializeField, Range(0f, 1f)]
    private float progress = 0f;

    [SerializeField]
    private float screwInSpeed = 1f;

    [SerializeField]
    private float radius = 0.5f;

    [SerializeField]
    private float screwedInDistance = 1f;

    [SerializeField]
    private Transform top;

    [SerializeField]
    private AnimationCurve screwCurve = new AnimationCurve();

    private Vector3 originalPosition;

    /// <summary>
    /// The rotation of the screw on its local Y axis.
    /// </summary>
    public float Rotation { get; set; }

    public Vector3 Top => top.position;
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
        originalPosition = transform.localPosition;
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
        Vector3 nailedInPosition = originalPosition - transform.up * screwedInDistance;
        transform.localPosition = Vector3.Lerp(originalPosition, nailedInPosition, screwCurve.Evaluate(Percentage));
        transform.localEulerAngles = new Vector3(0f, Rotation, 0f);
    }
}
