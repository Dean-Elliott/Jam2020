using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public LazySuzie LazySuzie;

    private void FixedUpdate()
    {
        LazySuzie.ForwardY = false;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.GetComponentInParent<Player>())
        {
            LazySuzie.ForwardY = true;
        }
    }
}
