using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static int finalScore;

    private static GameManager instance;

    public Color colorRed = Color.red;
    public Color colorYellow = Color.yellow;
    public Color colorBlue = Color.blue;
    public GameObject[] toys;
    private int currentToy = 0;

    private float totalScore;

    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI timerText;

    private ToyController toyControllerComponent;

    public LazySuzie lazySusan;

    public float timeLimit;
    private float elapsingTime;

    // Start is called before the first frame update
    void Start()
    {
        finalScore = 0;

        //DontDestroyOnLoad(gameObject);

        toyControllerComponent = toys[0].GetComponent<ToyController>();
        elapsingTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsingTime > 0.0f)
        {
            elapsingTime -= Time.deltaTime;
        }
        else
        {
            elapsingTime = 0.0f;
        }

        timerText.text = "" + System.Math.Round(elapsingTime, 2);

        toys[currentToy].transform.rotation = lazySusan.transform.rotation;

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
