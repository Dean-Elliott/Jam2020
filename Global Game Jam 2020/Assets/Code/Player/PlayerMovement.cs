using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 3f;

    [SerializeField]
    private float accelerationFactor = 100f;

    [SerializeField]
    private float decelerationFactor = 16f;

    [SerializeField]
    private float gravity = 16f;

    [SerializeField]
    private float jumpHeight = 3f;

    private float nextGroundLeave;

    /// <summary>
    /// The input that this player should move with.
    /// </summary>
    public Vector2 Input { get; set; }

    /// <summary>
    /// Should the player jump right now?
    /// </summary>
    public bool Jump { get; set; }

    /// <summary>
    /// Is this player moving according to the input?
    /// </summary>
    public bool IsMoving => Input.sqrMagnitude > 0.5f;

    /// <summary>
    /// Is the player currently grounded?
    /// </summary>
    public bool IsGrounded { get; private set; }

    public bool Gravity { get; set; }

    /// <summary>
    /// The rigidbody attached to this player.
    /// </summary>
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.useGravity = false;
        Rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        //get velocity, lerp it to destination velocity, then assign it back
        Vector3 velocity = Rigidbody.velocity;
        Vector3 desiredVelocity = new Vector3(Input.x, 0f, Input.y).normalized * movementSpeed;
        desiredVelocity.y = velocity.y;

        float acceleration = IsMoving ? accelerationFactor : decelerationFactor;
        velocity = Vector3.Lerp(velocity, desiredVelocity, Time.fixedDeltaTime * acceleration);
        Rigidbody.velocity = velocity;

        if (Gravity)
        {
            ApplyGravity();
        }

        //leave ground after this timer
        if (Time.fixedTime > nextGroundLeave)
        {
            IsGrounded = false;
        }
    }

    private void Update()
    {
        //do jump check
        if (Jump && IsGrounded)
        {
            PerformJump();
        }
    }

    /// <summary>
    /// Performs a jump, duh.
    /// </summary>
    public void PerformJump()
    {
        IsGrounded = false;
        Vector3 velocity = Rigidbody.velocity;
        velocity.y = Mathf.Sqrt(2f * gravity * jumpHeight);
        Rigidbody.velocity = velocity;
    }

    private void ApplyGravity()
    {
        Rigidbody.AddForce(Vector3.down * gravity);
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            float angle = Vector3.Angle(contact.normal, Vector3.up);

            //tis the slope angle
            if (angle <= 45f)
            {
                IsGrounded = true;
                nextGroundLeave = Time.fixedTime + 0.15f;
            }
        }
    }
}