using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timeText;
    [SerializeField]
    private TMP_Text bestTimeText;

    #region Buttons
    public void OnRetryBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuBtnClick()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    public void ShowResult(float time, float bestTime)
    {
        timeText.text = "TIME : " + time.ToString("F");
        bestTimeText.text = "BEST TIME : " + bestTime.ToString("F");
    }
}
