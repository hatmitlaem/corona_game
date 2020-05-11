using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Superpow;

public class ClassicController : MonoBehaviour
{

    public GameObject gameOverTitle, scoreObj, levelObj;
    public Text scoreText, levelText, gameOverScoreText, bestScoreText;
    private int score, currLevel;
    private double lastTimeScore;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }

    public void UpdateLevel(int currentLevel)
    {
        currLevel = currentLevel;
        levelText.text = currentLevel == 0 ? "1" : currentLevel.ToString();
    }

    public void InitClassic()
    {
        Score = 0;
        currLevel = 0;
        lastTimeScore = CUtils.GetCurrentTime();
    }

    public void ResetTimeScore()
    {
        lastTimeScore = CUtils.GetCurrentTime();
    }

    public void ShowLevelAndScore(bool isShow)
    {
        scoreObj.SetActive(isShow);
        levelObj.SetActive(isShow);
    }

    public void ShowGameOver(bool isShow)
    {
        gameOverTitle.SetActive(isShow);
        gameOverScoreText.text = score.ToString();
        if (isShow)
        {
            UpdateRank(score);
        }
        bestScoreText.text = Utils.GetBestScore().ToString();
    }

    private void UpdateRank(int score)
    {
        List<int> scoreList = new List<int>();
        scoreList.Add(Utils.GetBestScore());
        scoreList.Add(Utils.Get2ndScore());
        scoreList.Add(Utils.Get3rdScore());
        scoreList.Add(Utils.Get4thScore());
        scoreList.Add(Utils.Get5thScore());

        for (int i = 0; i < scoreList.Count; i++)
        {
            if (scoreList[i] < score)
            {
                for (int j = scoreList.Count - 1; j > i; j--)
                {
                    scoreList[j] = scoreList[j - 1];
                }
                scoreList[i] = score;
                break;
            }
        }
        Utils.SetBestScore(scoreList[0]);
        Utils.Set2ndScore(scoreList[1]);
        Utils.Set3rdScore(scoreList[2]);
        Utils.Set4thScore(scoreList[3]);
        Utils.Set5thScore(scoreList[4]);
    }
    private void FixedUpdate()
    {
        if (MainController.IsPlaying())
        {
            if (currLevel >= 1 && CUtils.GetCurrentTime() - lastTimeScore >= 1)
            {
                Score++;
                lastTimeScore = CUtils.GetCurrentTime();
            }
        }
    }
}
