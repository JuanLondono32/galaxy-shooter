using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImage;
    public Text scoreText;
    public GameObject titleScreen;
    public int score;

    public void UpdateLives(int currentLives)
    {
        livesImage.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score = score + 10;
        scoreText.text = "Score: " + score;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: ";
    }

}
