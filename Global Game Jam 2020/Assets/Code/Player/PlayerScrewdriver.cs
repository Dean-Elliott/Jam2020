using UnityEngine;

public class PlayerScrewdriver : Player
{
    [SerializeField]
    private float animationDuration = 0.6f;

    [SerializeField]
    private AnimationCurve positionCurve;

    [SerializeField]
    private float distanceToTip = 1f;

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
        Gizmos.DrawWireSphere(transform.position - transform.up * distanceToTip, 0.8f);
    }

    protected override void OnUpdate()
    {
        if (IsScrewingIn)
        {
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
                float amount = (LeftStick - lastStickDir).magnitude;
                lastStickDir = LeftStick;
                closeScrew.ScrewIn(amount);
                if (closeScrew.Percentage < 1f)
                {
                    closeScrew.Rotation = Rotation;
                }

                Gamepad.SetMotorSpeeds(0f, amount * 3f);
            }

            Quaternion oiler = closeScrew.transform.rotation;
            transform.rotation = oiler;
            Visual.localEulerAngles = new Vector3(0f, Rotation, 0f);

            //released the left trigger
            if (Trigger < 0.2f)
            {
                IsScrewingIn = false;
                CanMove = true;
                closeScrew = null;
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                Visual.localEulerAngles = Vector3.zero;
            }

            return;
        }
        else
        {
            Gamepad.ResetHaptics();
        }

        //find closest screw
        closeScrew = GetClosestScrew();
        if (closeScrew && Trigger > 0.2f)
        {
            screwingTime = 0f;
            IsScrewingIn = true;
            CanMove = false;
            lastStickDir = LeftStick;
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
            if (dist < closestDistance && dist < 1f * 1f)
            {
                closestDistance = dist;
                closestScrew = screw;
            }
        }

        return closestScrew;
    }
}