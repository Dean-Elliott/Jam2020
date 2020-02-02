using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [HideInInspector]
    public float elapsingBonusTime;

    private List<IInteractable> parts = new List<IInteractable>();

    // Start is called before the first frame update
    void Start()
    {
        animatorComponent = gameObject.GetComponent<Animator>();
        elapsingBonusTime = bonusTimeLimit;

        parts = GetComponentsInChildren<IInteractable>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && developerModeActive == true)
        {
            ToyCompleted();
        }

        elapsingBonusTime -= Time.deltaTime;

        if (elapsingBonusTime < 0.0f)
        {
            elapsingBonusTime = 0.0f;
        }

        transform.position = enterPosition;

        //check when a part is 100% donesies
        float percentage = 0f;
        for (int i = parts.Count - 1; i >= 0; i--)
        {
            IInteractable part = parts[i];
            percentage += part.Percentage;

            //if (part.Percentage == 1f)
            //{
            //    //part reached 100
            //    //add score and removeth from list
            //    parts.RemoveAt(i);
            //    AddPieceScore(1f);
            //}
        }

        percentage /= parts.Count;
    }

    public void AddPieceScore(float pieceScore)
    {
        totalToyScore += pieceScore;
    }

    public void ToyCompleted()
    {
        totalToyScore += (float)System.Math.Round(elapsingBonusTime, 0) + paintAccuracy;
        animatorComponent.SetBool("ToyCompleted", true);

        Debug.Log("Total toy score: " + totalToyScore);
    }

    public void TransitionToNewToy()
    {
        owner.SendMessage("ActivateNewToy", SendMessageOptions.RequireReceiver);
        gameObject.SetActive(false);
    }


}
