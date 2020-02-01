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

    private void OnEnable()
    {
        ForwardY = false;
        ReverseY = false;
    }
    void Update()
    {


        //Forward Direction

        if (ForwardY == true)
        {
            transform.Rotate(0, Time.deltaTime * speed, 0, Space.Self);
        }
     
        //Reverse Direction
     
        if (ReverseY == true)
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

