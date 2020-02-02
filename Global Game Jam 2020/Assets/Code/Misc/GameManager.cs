using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public Color colorRed = Color.red;
    public Color colorYellow = Color.yellow;
    public Color colorBlue = Color.blue;
    public GameObject[] toys;
    private int currentToy = 0;

    private float totalScore;

    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI bonusTimerText;

    private ToyController toyControllerComponent;

    public LazySuzie lazySusan;

    // Start is called before the first frame update
    void Start()
    {
        toyControllerComponent = toys[0].GetComponent<ToyController>();
    }

    // Update is called once per frame
    void Update()
    {
        toys[currentToy].transform.rotation = lazySusan.transform.rotation;

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
            lazySusan.reset = true;
            toys[currentToy + 1].SetActive(true);
            currentToy++;
            toyControllerComponent = toys[currentToy].GetComponent<ToyController>();
        }
    }

    public void FinishGame()
    {
        Debug.Log("Game Finished!");
    }

    public static Color GetColor(ColorType colorType)
    {
        if (!instance)
        {
            instance = FindObjectOfType<GameManager>();
        }

        if (colorType == ColorType.Blue)
        {
            return instance.colorBlue;
        }
        else if (colorType == ColorType.Red)
        {
            return instance.colorRed;
        }
        else if (colorType == ColorType.Yellow)
        {
            return instance.colorYellow;
        }

        return Color.white;
    }
}
