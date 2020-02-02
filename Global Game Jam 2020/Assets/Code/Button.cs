using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public LazySuzie LazySuzie;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.transform.parent.tag == "Player")
        {
            LazySuzie.ForwardY = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.transform.parent.tag == "Player")
        {
            LazySuzie.ForwardY = false;
        }
    }

}
