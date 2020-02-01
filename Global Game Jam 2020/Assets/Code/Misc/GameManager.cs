using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] toys;
    private int currentToy = 0;

    private float totalScore;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bonusTimerText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateNewToy()
    {
        if (currentToy == toys.Length - 1)
        {
            FinishGame();
        }
        else
        {
            toys[currentToy + 1].SetActive(true);
            currentToy++;
        }
    }

    public void FinishGame()
    {
        Debug.Log("Game Finished!");
    }
}
