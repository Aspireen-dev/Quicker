using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text bestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateScore(int score, int bestScore)
    {
        scoreText.text = "SCORE :\r\n" + score.ToString();
        bestScoreText.text = "BEST SCORE :\r\n" + bestScore.ToString();
    }

    public void OnMenuBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
