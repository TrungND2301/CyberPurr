using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    int currentScore;
    int startScore = 0;

    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        currentScore = startScore;
        scoreText.text = currentScore.ToString();
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Mathf.Clamp(currentScore, 0, int.MaxValue);
        scoreText.text = currentScore.ToString();
    }

    public void ResetScore()
    {
        currentScore = startScore;
        scoreText.text = currentScore.ToString();
    }
}
