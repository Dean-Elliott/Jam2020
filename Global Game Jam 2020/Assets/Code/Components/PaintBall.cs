using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBall : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public new Renderer renderer;

    private bool stuck;

    private void FixedUpdate()
    {
        rigidbody.AddForce(Vector3.down * 30f);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stuck)
        {
            return;
        }

        //as long as it didnt collide with a player, or another paint ball
        Player player = other.GetComponentInParent<Player>();
        if (!player)
        {
            PaintBall ball = other.GetComponentInParent<PaintBall>();
            if (!ball)
            {
                rigidbody.isKinematic = true;
                stuck = true;
                Destroy(gameObject, 30f);
            }
        }
    }
}