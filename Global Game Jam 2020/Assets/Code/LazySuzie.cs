using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazySuzie : MonoBehaviour
{
    //Rotational Speed
    public float speed = 0f;

    //Forward Direction
    public bool ForwardY = false;

    //Reverse Direction
    public bool ReverseY = false;

    public bool reset;
    public float resetSpeed;

    private void OnEnable()
    {
        ForwardY = false;
        ReverseY = false;
    }
    void Update()
    {
        if (reset == true)
        {
            Vector3 stopPosition = new Vector3(0.0f, 0.0f, 0.0f);
            if (Vector3.Distance(transform.eulerAngles, stopPosition) > 0.01f)
            {
                transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, stopPosition, 10 * Time.deltaTime);
            }
            else
            {
                transform.eulerAngles = stopPosition;
                reset = false;
            }
        }

        //Forward Direction
        if (ForwardY == true && reset == false)
        {
            transform.Rotate(0, Time.deltaTime * speed, 0, Space.Self);
        }

        //Reverse Direction     
        if (ReverseY == true && reset == false)
        {
            transform.Rotate(0, -Time.deltaTime * speed, 0, Space.Self);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Button")
        {
            ForwardY = true;


        }
        else
        {
            ForwardY = false;
        }
    }
}

