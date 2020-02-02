using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnButtonPressedEvent : UnityEvent { }


public class NewToyButton : MonoBehaviour
{
    public OnButtonPressedEvent OnButtonPressed = new OnButtonPressedEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnButtonPressed.Invoke();
        }
    }
}
