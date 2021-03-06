﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isStartScreen = true;

    [HideInInspector]
    public static int finalScore;

    private static GameManager instance;

    public Color colorRed = Color.red;
    public Color colorYellow = Color.yellow;
    public Color colorBlue = Color.blue;
    public GameObject[] toys;
    private int currentToy = 0;

    public float totalScore;

    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI timerText;

    private ToyController toyControllerComponent;

    public LazySuzie lazySusan;

    public float timeLimit;
    [HideInInspector]
    public float elapsingTime;
    private string minutes;
    private string seconds;

    // Start is called before the first frame update
    void Start()
    {
        finalScore = 0;

        //DontDestroyOnLoad(gameObject);

        if (isStartScreen == false)
        {
            toyControllerComponent = toys[0].GetComponent<ToyController>();
        }
        elapsingTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartScreen == false)
        {
            if (elapsingTime > 0.0f)
            {
                elapsingTime -= Time.deltaTime;
            }
            else
            {
                GameOver();
                elapsingTime = 0.0f;
            }
        }

        if (isStartScreen == false)
        {
            minutes = Mathf.Floor(elapsingTime / 60).ToString("00");
            seconds = Mathf.Floor(elapsingTime % 60).ToString("00");

            timerText.text = string.Format("{0}:{1}", minutes, seconds);
        }

        if (isStartScreen == false)
        {
            toys[currentToy].transform.rotation = lazySusan.transform.rotation;
        }

        /*
        if (timerText != null)
        {
            bonusTimerText.text = "Bonus Timer: " + System.Math.Round(toyControllerComponent.elapsingBonusTime, 2);
        }
        */

        //set score to the score on the toy
        if (toyControllerComponent)
        {
            totalScoreText.text = "Total Score: " + toyControllerComponent.totalToyScore;
        }
        else
        {
            totalScoreText.text = "Total Score: " + totalScore;
        }
        //bonusTimerText.text = "" + System.Math.Round(toyControllerComponent.elapsingBonusTime, 2);
        totalScoreText.text = "" + totalScore;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("NewEndScene", LoadSceneMode.Single);
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
        finalScore = (int)elapsingTime * 100;

        SceneManager.LoadScene("NewEndScene", LoadSceneMode.Single);
        Debug.Log("Game Finished!");
    }

    public void CompleteCurrentToy()
    {

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
