using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverAnim : MonoBehaviour
{
    public PlayerScrewdriver PlayerS;
    public PlayerMovement Player;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        anim.SetBool("IsScrewing", PlayerS.IsScrewingIn);
        
        anim.SetBool("IsWalking", Player.IsMoving);
    }
}
