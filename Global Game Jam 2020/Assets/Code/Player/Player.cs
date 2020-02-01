using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int id = 0;

    [SerializeField]
    private Transform visual;

    /// <summary>
    /// The movement component attached to this player.
    /// </summary>
    public PlayerMovement Movement { get; private set; }

    /// <summary>
    /// Can the player look around using the left stick?
    /// </summary>
    public bool CanLookAround { get; set; } = true;

    public bool CanMove { get; set; } = true;

    public Transform Visual => visual;

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
        OnAwake();
    }

    protected virtual void OnAwake()
    {

    }

    public Gamepad Gamepad
    {
        get
        {
            if (id >= 0 && id < Gamepad.all.Count)
            {
                return Gamepad.all[id];
            }

            return null;
        }
    }

    public float LeftTrigger
    {
        get
        {
            return Gamepad?.leftTrigger?.ReadValue() ?? (Keyboard.current.qKey.isPressed ? 1 : 0);
        }
    }

    public float RightTrigger
    {
        get
        {
            return Gamepad?.rightTrigger?.ReadValue() ?? (Keyboard.current.eKey.isPressed ? 1 : 0);
        }
    }

    public Vector2 LeftStick
    {
        get
        {
            Gamepad gamepad = Gamepad;
            if (gamepad != null)
            {
                return gamepad.leftStick.ReadValue();
            }
            else
            {
                Vector2 vector = default;
                if (Keyboard.current.wKey.isPressed)
                {
                    vector.y = 1;
                }
                else if (Keyboard.current.aKey.isPressed)
                {
                    vector.x = -1;
                }
                else if (Keyboard.current.sKey.isPressed)
                {
                    vector.y = -1;
                }
                else if (Keyboard.current.dKey.isPressed)
                {
                    vector.x = 1;
                }

                return vector;
            }
        }
    }

    public Vector2 RightStick
    {
        get
        {
            Gamepad gamepad = Gamepad;
            if (gamepad != null)
            {
                return gamepad.rightStick.ReadValue();
            }

            return default;
        }
    }

    private void Update()
    {
        //send inputs to the movement thingy
        float x = LeftStick.x;
        float y = LeftStick.y;
        bool jump = Gamepad?.buttonSouth?.isPressed ?? Keyboard.current.spaceKey.isPressed;
        Vector2 input = new Vector2(x, y);
        Movement.Input = CanMove ? input : default;
        Movement.Jump = CanMove ? jump : default;

        if (CanLookAround)
        {
            LookAround();
        }

        OnUpdate();
    }

    protected virtual void OnUpdate()
    {

    }

    private void LookAround()
    {
        //looking around
        float x = LeftStick.x;
        float y = LeftStick.y;

        //check if joystick looked far enough
        Vector3 lookDir = new Vector3(x, y);
        if (lookDir.sqrMagnitude > 0.25f)
        {
            //normalize the thingy
            lookDir.Normalize();

            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.y = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = eulerAngles;
        }
    }
}
