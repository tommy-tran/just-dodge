using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    int score;
    public GameObject scoreText;
    public GameObject gameOverScoreText;
    public PlayerController playerController;
    public SoundController soundController;
    public GameObject usernameInput;
    public HighScores highscores;
    public GameObject submitButton;

    public void sendScore()
    {
        string username = usernameInput.GetComponent<InputField>().text;
        if (username.Length > 2 && score > 0)
        {
            highscores.AddNewHighScore(username, score);
            submitButton.SetActive(false);
        }
    }

    private void Start()
    {
        score = 0;
    }

    public void gotCoin()
    {
        score += 5;
        updateScore();
        soundController.playCoinSound();
    }

    public void dodgedEnemy()
    {
        score++;
        updateScore();
    }

    public void resetScore()
    {
        score = 0;
    }

    void updateScore()
    {
        scoreText.GetComponent<Text>().text = (score).ToString();
    }

    public void setScore(bool bossDefeated)
    {
        if (bossDefeated) { score += 400; }
        score += playerController.health * 200;
        gameOverScoreText.GetComponent<Text>().text = (score).ToString();
    }
}
