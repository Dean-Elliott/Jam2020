﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyController : MonoBehaviour
{
    private Animator animatorComponent;

    public GameObject owner;

    [HideInInspector]
    public float totalToyScore;

    public bool developerModeActive = false;

    public Vector3 enterPosition;

    private float paintAccuracy;

    public float bonusTimeLimit;
    private float elapsingBonusTime;

    // Start is called before the first frame update
    void Start()
    {
        animatorComponent = gameObject.GetComponent<Animator>();

        elapsingBonusTime = bonusTimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && developerModeActive == true)
        {
            ToyCompleted();
        }

        elapsingBonusTime -= Time.deltaTime;

        //Debug.Log(elapsingBonusTime);

        if (elapsingBonusTime < 0.0f)
        {
            elapsingBonusTime = 0.0f;
        }

        transform.position = enterPosition;
    }

    public void AddPieceScore(float pieceScore)
    {
        totalToyScore += pieceScore;
    }

    public void ToyCompleted()
    {
        totalToyScore += elapsingBonusTime + paintAccuracy;
        animatorComponent.SetBool("ToyCompleted", true);

        Debug.Log("Total toy score: " + totalToyScore);
    }

    public void SetEnterPosition()
    {
        Debug.Log("Enter position set");
        transform.position = enterPosition;
    }

    public void TransitionToNewToy()
    {
        owner.SendMessage("ActivateNewToy", SendMessageOptions.RequireReceiver);
        gameObject.SetActive(false);
    }


}
