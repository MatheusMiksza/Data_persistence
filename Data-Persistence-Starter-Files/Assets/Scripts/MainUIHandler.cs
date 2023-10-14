using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainUIHandler : MonoBehaviour
{
    public Text ScoreText;
    public Text bestScoreText;
    public Text gameOverText;

    public GameObject GameOverGB;
    public void OnClickExitMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowGameOver(bool b)
    {
        gameOverText.text = $"GAME OVER\nVoce conseguiu: {GameManeger.Instance.matchScore} pontos \nPressione espaço para Restart";
        GameOverGB.SetActive(b);
    }
}
