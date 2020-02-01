using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGManager : MonoBehaviour
{
    public static BGManager i;
    public Text scoreText;
    public int _score;

    public GameOverPanel gameOver;
    public GameObject score;

    private void Awake()
    {
        i = this;
        Application.targetFrameRate = 30;
    }

    public void FireScore()
    {
        _score += DB.i.fireScore;
        scoreText.NumberTween(_score, 0.2f);
    }

    public void GameEnd()
    {
        _score += DB.i.destroyCoreScore;
        gameOver.gameObject.SetActive(true);
        gameOver.score.NumberTween(_score, 1f);
        score.SetActive(false);

    }

    public void ScoreReset()
    {
        _score = 0;
        scoreText.text = "0";
    }


}
