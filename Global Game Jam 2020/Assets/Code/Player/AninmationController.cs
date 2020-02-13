using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AninmationController : MonoBehaviour
{
    public PlayerHammer PlayerH;
    public PlayerMovement Player;
    private Animator anim;
    private bool IsJump;
    private float mTime;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mTime = 0;
        IsJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        


        anim.SetBool("IsHammering", PlayerH.IsHammering);
        anim.SetBool("IsPullingBack", PlayerH.wasPullingBack);
        anim.SetBool("IsWalking", Player.IsMoving);
        
        anim.SetFloat("JumpTime", mTime);
        
        if (Player.Jump == true)
        {
            IsJump = true;
            anim.SetBool("IsJumping", true);

        }

        if (IsJump == true)
        {
            StartTimer();
            if (mTime >= 1)
            {
                StopTimer();
                ResetTimer();
                IsJump = false;
            }
        }
        
        

        
    }
    void StartTimer()
    {
        mTime += Time.deltaTime;
    }
    void StopTimer()
    {
        anim.SetBool("IsJumping", false);
        mTime = mTime + 0;
    }
    void ResetTimer()
    {
        mTime = 0;
    }
    
}
