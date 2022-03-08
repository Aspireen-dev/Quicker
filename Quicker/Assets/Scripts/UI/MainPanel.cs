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
    private GameObject nbTargets;
    [SerializeField]
    private TMP_Text timeText;

    [SerializeField]
    private EndGamePanel endGamePanel;

    [SerializeField]
    private GameManager gameManager;

    public void OnPlayBtnClick(GameObject btn)
    {
        btn.SetActive(false);
        nbTargets.SetActive(true);
    }

    public void OnNbTargetsClick(int nbTargets)
    {
        gameManager.StartGame(nbTargets);
    }

    public void HideMenu()
    {
        menu.SetActive(false);
        timeText.gameObject.SetActive(true);
    }

    public void OnTargetBtnClick()
    {
        gameManager.TargetHit();
    }

    public void EndGame(float time, float bestTime)
    {
        endGamePanel.gameObject.SetActive(true);
        endGamePanel.ShowResult(time, bestTime);
    }

    public void UpdateTime(float time)
    {
        timeText.text = time.ToString("F");
    }
}
