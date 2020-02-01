using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public LazySuzie LazySuzie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            LazySuzie.ForwardY = true;


        }
        /*
        else
        {
           LazySuzie.ForwardY = false;
        }
        */
    }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.tag == "Player")
                {
                LazySuzie.ForwardY = false;
            }
               
            
        }
    
}
