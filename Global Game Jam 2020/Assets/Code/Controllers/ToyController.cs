using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToyController : MonoBehaviour
{
    private Animator animatorComponent;

    public GameObject owner;

    public float totalToyScore;

    public bool developerModeActive = false;

    public Vector3 enterPosition;

    private float paintAccuracy;

    //public float bonusTimeLimit;

    [HideInInspector]
    public float elapsingBonusTime;

    private List<IInteractable> parts = new List<IInteractable>();
    private bool isCompleted;

    public AudioSource audioSource;
    public AudioClip[] shoot;
    public AudioClip shootClip;

    // Start is called before the first frame update
    void Start()
    {
        animatorComponent = gameObject.GetComponent<Animator>();
        //elapsingBonusTime = bonusTimeLimit;

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



        //calculate total score
        if (!isCompleted)
        {
            totalToyScore = 0f;
            foreach (var part in parts)
            {
                totalToyScore += part.Percentage;
            }

            if (totalToyScore == parts.Count)
            {
                ToyCompleted();
            }
        }
    }

    public void ToyCompleted()
    {
        int index = Random.Range(0, shoot.Length);
        shootClip = shoot[index];
        audioSource.clip = shootClip;
        audioSource.Play();

        isCompleted = true;
        //totalToyScore += (float)System.Math.Round(elapsingBonusTime, 0) + paintAccuracy;
        animatorComponent.SetBool("ToyCompleted", true);

        Debug.Log("Total toy score: " + totalToyScore);
    }

    public void TransitionToNewToy()
    {
        owner.SendMessage("ActivateNewToy", SendMessageOptions.RequireReceiver);
        gameObject.SetActive(false);
    }


}
