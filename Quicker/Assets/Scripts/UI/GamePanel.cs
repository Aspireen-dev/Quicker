using TMPro;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private EndGamePanel endGamePanel;

    [SerializeField]
    private TMP_Text timeText;
    [SerializeField]
    private GameObject targetBtn;

    // RectTransform values
    private float targetWidth;
    private float widthLimit;
    private float heightLimit; 
    private float topOffset = 250f;


    // Start is called before the first frame update
    void Start()
    {
        targetWidth = targetBtn.GetComponent<RectTransform>().rect.width;
        Rect screen = GameObject.Find("Canvas").GetComponent<RectTransform>().rect;
        widthLimit = (screen.width / 2) - targetWidth;
        heightLimit = (screen.height / 2) - targetWidth;
    }

    #region Buttons
    public void OnTargetBtnClick()
    {
        gameManager.TargetHit();
    }
    #endregion

    public void UpdateTime(float time)
    {
        timeText.text = time.ToString("F");
    }

    public void SetNewTargetPosition()
    {
        float randomWidth = Random.Range(-widthLimit, widthLimit);
        float randomHeight = Random.Range(-heightLimit, heightLimit - topOffset); // Offset on the top of the screen (250px)
        targetBtn.transform.localPosition = new Vector3(randomWidth, randomHeight, 0);
    }

    public void EndGame(float time, float bestTime)
    {
        targetBtn.SetActive(false);
        endGamePanel.gameObject.SetActive(true);
        endGamePanel.ShowResult(time, bestTime);
    }
}
