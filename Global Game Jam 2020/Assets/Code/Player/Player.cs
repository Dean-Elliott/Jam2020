using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int id = 0;

    /// <summary>
    /// The movement component attached to this player.
    /// </summary>
    public PlayerMovement Movement { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
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

    private void Update()
    {
        //send inputs to the movement thingy
        float x = Gamepad.leftStick.x.ReadValue();
        float y = Gamepad.leftStick.y.ReadValue();
        bool jump = Gamepad.buttonSouth.isPressed;
        Vector2 input = new Vector2(x, y);
        Movement.Input = input;
        Movement.Jump = jump;

        //looking around
        x = Gamepad.rightStick.x.ReadValue();
        y = Gamepad.rightStick.y.ReadValue();

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
