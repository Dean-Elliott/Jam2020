using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        finalScoreText.text = "" + GameManager.finalScore;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
