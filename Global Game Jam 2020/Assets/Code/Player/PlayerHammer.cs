using System.Threading.Tasks;
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
    public bool wasPullingBack;

    public bool IsHammering { get; private set; }

    protected override void OnUpdate()
    {
        if (IsHammering)
        {
            CanLookAround = false;
            Movement.Gravity = false;
            CanMove = false;

            //animate here
            animationTime += Time.deltaTime;
            float angle = hammerSwingCurve.Evaluate(animationTime / animationDuration);
            Visual.localEulerAngles = new Vector3(angle * 90f, 0f, 0f);

            if (animationTime >= animationDuration)
            {
                IsHammering = false;
                CanLookAround = true;
                CanMove = true;
                Movement.Gravity = true;
            }

            return;
        }

        HighlightNearbyNails();

        //pull back based on trigger
        if (Trigger > 0.1f)
        {
            wasPullingBack = true;
            CanMove = false;

            //swing back lmao
            float angle = pullbackAngle.Evaluate(Trigger);
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

    /// <summary>
    /// Vibrates for a bit.
    /// </summary>
    private async void Vibrate(float duration = 0.1f)
    {
        Gamepad?.SetMotorSpeeds(1f, 0f);
        int ms = (int)(duration * 1000);
        await Task.Delay(ms);
        Gamepad?.ResetHaptics();
    }

    private void Hammer()
    {
        animationTime = 0f;
        IsHammering = true;
        CanMove = false;
        Movement.Gravity = false;
        Movement.Rigidbody.velocity = Vector3.zero;

        Vibrate();
        LookTowardsNearbyNail();
    }

    private Nail GetNearbyNail()
    {
        //max assist angle
        float maxAngle = 40f;

        //max distance to check with
        float range = 3.2f;

        //snap to nearest nail lol
        Nail[] nails = FindObjectsOfType<Nail>();
        float closest = float.MaxValue;
        Nail closestNail = null;
        foreach (Nail nail in nails)
        {
            //get dir and flatten on Y, so that the math is done on the XZ plane
            Vector3 dirToNail = nail.transform.position - transform.position;
            dirToNail.y = 0f;

            //too far by distance
            if (dirToNail.sqrMagnitude > range * range)
            {
                continue;
            }

            //normalize later
            dirToNail.Normalize();

            float angle = Vector3.Angle(transform.forward, dirToNail);
            if (angle < closest && angle < maxAngle)
            {
                closest = angle;
                closestNail = nail;
            }
        }

        return closestNail;
    }

    private void HighlightNearbyNails()
    {
        Nail nail = GetNearbyNail();
        if (nail)
        {
            nail.Highlight();
        }
    }

    /// <summary>
    /// Tis the magnetize effect.
    /// </summary>
    private void LookTowardsNearbyNail()
    {
        Nail nail = GetNearbyNail();

        //look towards closest nail
        if (nail)
        {
            Vector3 dirToNail = (nail.transform.position - transform.position).normalized;
            Debug.DrawRay(transform.position, dirToNail, Color.yellow, 2f);

            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.y = Mathf.Atan2(dirToNail.x, dirToNail.z) * Mathf.Rad2Deg;
            transform.eulerAngles = eulerAngles;
        }
    }
}