using UnityEngine;

public class PlayerScrewdriver : Player
{
    [SerializeField]
    private float animationDuration = 0.6f;

    [SerializeField]
    private AnimationCurve positionCurve;

    [SerializeField]
    private float distanceToTip = 1f;

    [SerializeField]
    private float radius = 1.35f;

    private Vector3 originalPosition;
    private Vector3 middlePosition;
    private float screwingTime;
    private Screw closeScrew;
    private Vector2 lastStickDir;
    private Vector3 boundsSizes;

    public bool IsScrewingIn { get; private set; }

    protected override void OnAwake()
    {
        Bounds bounds = new Bounds(transform.position, default);
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            bounds.Encapsulate(collider.bounds);
        }

        boundsSizes = bounds.size;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - transform.up * distanceToTip, radius);
    }

    protected override void OnUpdate()
    {
        if (IsScrewingIn)
        {
            CanLookAround = false;
            screwingTime += Time.deltaTime;
            float t = positionCurve.Evaluate(Mathf.Clamp01(screwingTime / animationDuration));
            if (t > 0.5f)
            {
                //go towards thingy
                transform.position = Vector3.Lerp(middlePosition, closeScrew.Top + closeScrew.transform.up * distanceToTip, (t - 0.5f) * 2f);
            }
            else
            {
                //go towards middle
                transform.position = Vector3.Lerp(originalPosition, middlePosition, t * 2f);
            }

            //come on, screw it
            if (t >= 1f)
            {
                float amount = (RightStick - lastStickDir).magnitude;
                lastStickDir = RightStick;
                closeScrew.ScrewIn(amount);
                if (closeScrew.Percentage < 1f)
                {
                    Quaternion oiler = closeScrew.transform.rotation;
                    transform.rotation = oiler;
                    Visual.localEulerAngles = new Vector3(0f, GetRotation(RightStick), 0f);

                    closeScrew.Rotation = GetRotation(RightStick);
                    Gamepad.SetMotorSpeeds(0f, amount * 0.5f);
                }
                else
                {
                    Gamepad.SetMotorSpeeds(amount, amount * 5f);
                }
            }

            //released the left trigger
            if (Trigger < 0.2f)
            {
                IsScrewingIn = false;
                CanMove = true;
                CanLookAround = true;
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                Visual.localEulerAngles = new Vector3(0f, -90f, 0f);
                transform.position = closeScrew.OriginalTop + closeScrew.transform.up * (distanceToTip + 1f);
                closeScrew = null;
            }

            return;
        }
        else
        {
            Gamepad?.ResetHaptics();
        }

        //find closest screw
        closeScrew = GetClosestScrew();
        if (closeScrew && Trigger > 0.2f)
        {
            screwingTime = 0f;
            IsScrewingIn = true;
            CanMove = false;
            CanLookAround = false;
            lastStickDir = RightStick;
            originalPosition = transform.position;

            //get middle pos by doing avg of 2 positions + half angle of driver to screw
            middlePosition = (closeScrew.Top + originalPosition) * 0.5f;
            middlePosition += transform.up + closeScrew.transform.up;
        }
    }

    private Screw GetClosestScrew()
    {
        Screw[] screws = FindObjectsOfType<Screw>();
        float closestDistance = float.MaxValue;
        Screw closestScrew = null;
        foreach (Screw screw in screws)
        {
            float dist = (screw.Top - transform.position).sqrMagnitude;
            if (dist < closestDistance && dist < radius * radius)
            {
                closestDistance = dist;
                closestScrew = screw;
            }
        }

        return closestScrew;
    }
}