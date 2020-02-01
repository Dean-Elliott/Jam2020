using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] toys;
    private int currentToy = 0;

    private float totalScore;

    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI bonusTimerText;

    private ToyController toyControllerComponent;

    // Start is called before the first frame update
    void Start()
    {
        toyControllerComponent = toys[0].GetComponent<ToyController>();
    }

    // Update is called once per frame
    void Update()
    {
        bonusTimerText.text = "Bonus Timer: " + System.Math.Round(toyControllerComponent.elapsingBonusTime, 2);
        totalScoreText.text = "Total Score: " + totalScore;
    }

    public void ActivateNewToy()
    {
        totalScore += toyControllerComponent.totalToyScore;

        if (currentToy == toys.Length - 1)
        {
            FinishGame();
        }
        else
        {
            toys[currentToy + 1].SetActive(true);
            currentToy++;
            toyControllerComponent = toys[currentToy].GetComponent<ToyController>();
        }
    }

    public void FinishGame()
    {
        Debug.Log("Game Finished!");
    }
}
