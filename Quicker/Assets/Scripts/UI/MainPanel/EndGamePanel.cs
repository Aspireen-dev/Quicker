using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timeText;
    [SerializeField]
    private TMP_Text bestTimeText;

    public void ShowResult(float time, float bestTime)
    {
        timeText.text = "TIME :\r\n" + time.ToString("F");
        bestTimeText.text = "BEST TIME :\r\n" + bestTime.ToString("F");
    }

    public void OnMenuBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
