using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject playTimes;
    [SerializeField]
    private TMP_Text timeText;
    [SerializeField]
    private EndGamePanel endGamePanel;

    [SerializeField]
    private GameManager gameManager;

    public void OnPlayBtnClick(GameObject btn)
    {
        btn.SetActive(false);
        playTimes.SetActive(true);
    }

    public void OnNbSecClick(int duration)
    {
        gameManager.StartGame(duration);
    }

    public void HideMenu()
    {
        menu.SetActive(false);
        timeText.gameObject.SetActive(true);
    }

    public void EndGame(int score, int bestScore)
    {
        endGamePanel.gameObject.SetActive(true);
        endGamePanel.UpdateScore(score, bestScore);
    }

    /*public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }*/

    public void UpdateTime(float time)
    {
        timeText.text = time.ToString("F1");
    }
}
