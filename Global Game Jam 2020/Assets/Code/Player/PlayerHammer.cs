using System;
using UnityEngine;

public class PlayerHammer : Player
{
    [SerializeField]
    private float maxPullbackAngle = -80f;

    [SerializeField]
    private AnimationCurve pullbackAngle = new AnimationCurve();

    [SerializeField]
    private float animationDuration = 0.2f;

    [SerializeField]
    private AnimationCurve hammerSwingCurve = new AnimationCurve();

    private float animationTime;
    private bool wasPullingBack;

    public bool IsHammering { get; private set; }

    protected override void OnUpdate()
    {
        if (IsHammering)
        {
            CanLookAround = false;

            //animate here
            animationTime += Time.deltaTime;
            float angle = hammerSwingCurve.Evaluate(animationTime / animationDuration);
            Visual.localEulerAngles = new Vector3(angle * 90f, 0f, 0f);

            if (animationTime >= animationDuration)
            {
                IsHammering = false;
                CanLookAround = true;
            }

            return;
        }

        //pull back based on trigger
        if (LeftTrigger > 0.1f)
        {
            wasPullingBack = true;

            //swing back lmao
            float angle = pullbackAngle.Evaluate(LeftTrigger);
            Visual.localEulerAngles = new Vector3(angle * maxPullbackAngle, 0f, 0f);
        }
        else
        {
            if (wasPullingBack)
            {
                wasPullingBack = false;

                //hammer action
                Hammer();
            }
        }
    }

    private void Hammer()
    {
        animationTime = 0f;
        IsHammering = true;

        //snap to nearest nail lol
        Nail[] nails = FindObjectsOfType<Nail>();
        float closest = float.MaxValue;
        Nail closestNail = null;
        foreach (Nail nail in nails)
        {
            float dist = (nail.transform.position - transform.position).sqrMagnitude;
            if (dist > 2f * 2f)
            {
                continue;
            }

            Vector3 dirToNail = (transform.position - nail.transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToNail);
            if (angle < closest)
            {
                closest = angle;
                closestNail = nail;
            }
        }

        //look towards closest nail
        if (closestNail)
        {
            Vector3 dirToNail = (transform.position - closestNail.transform.position).normalized;
            Debug.DrawRay(transform.position, dirToNail, Color.yellow, 2f);

            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.y = Mathf.Atan2(dirToNail.y, dirToNail.x) * Mathf.Rad2Deg;
            transform.eulerAngles = eulerAngles;
        }
    }
}