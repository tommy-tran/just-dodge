using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    int score;
    int minionCount;
    public GameObject scoreText;
    public GameObject gameOverScoreText;
    public PlayerController playerController;
    public SoundController soundController;
    public GameObject usernameInput;
    public HighScores highscores;
    public GameObject submitButton;
    public GameObject popUpParent;
    public GameObject scorePopUp;
    Canvas canvas;
    Transform parentTransform;
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
        minionCount = 0;
        parentTransform = popUpParent.transform;
        canvas = GameObject.FindObjectOfType<Canvas>();
    }

    public void gotCoin(float x, float z)
    {
        score += 10;
        GameObject temp = Instantiate(scorePopUp, popUpParent.transform);
        temp.GetComponent<PointTextBehaviour>().text = "+10";
        updateScore();
        soundController.playCoinSound();
    }

    public void gotDiamond()
    {
        score += 100;
        GameObject temp = Instantiate(scorePopUp, popUpParent.transform);
        temp.GetComponent<PointTextBehaviour>().text = "+100";
        updateScore();
        soundController.playDiamondSound();
    }

    public void dodgedEnemy()
    {
        score++;
        updateScore();
    }

    public void dodgedMinion()
    {
        minionCount++;
        if (minionCount > 3)
        {
            score++;
            minionCount = 0;
        }
        updateScore();
    }

    public void resetScore()
    {
        score = 0;
        updateScore();
    }

    public void missedAlly()
    {
        score -= 50;
        GameObject temp = Instantiate(scorePopUp, popUpParent.transform);
        temp.GetComponent<PointTextBehaviour>().text = "-50";
        updateScore();
    }

    public void gotAlly()
    {
        score += 20;
        GameObject temp = Instantiate(scorePopUp, popUpParent.transform);
        temp.GetComponent<PointTextBehaviour>().text = "+20";
        updateScore();
    }

    void updateScore()
    {
        scoreText.GetComponent<Text>().text = (score).ToString();
        setScore();
    }

    public void win()
    {
        score += 350;
        score += playerController.health * 150;
        updateScore();
        setScore();
    }

    public void setScore()
    {
        gameOverScoreText.GetComponent<Text>().text = (score).ToString();
    }
}
