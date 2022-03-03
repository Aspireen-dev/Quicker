using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject targetPrefab; // Size of target : 150px * 150px
    [SerializeField]
    private MainPanel mainPanel;

    // Default values for 1080 * 1920 --- width and height divided by 2 and minus 100
    private float widthLimit = 440f;
    private float heightLimit = 860f;

    private bool isPlaying = false;

    private int score = 0;
    private float time; // 10 - 20 - 30 seconds modes
    private float timeLeft;

    private GameObject lastTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        widthLimit = (Screen.width / 2) - 100f;
        heightLimit = (Screen.height / 2) - 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            // ----- INPUTS -----
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
#if UNITY_EDITOR || UNITY_STANDALONE
                Vector3 touchPositionOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID
            Vector3 touchPositionOnScreen = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
                Vector2 touchPosition = new Vector2(touchPositionOnScreen.x, touchPositionOnScreen.y);

                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null)
                {
                    GameObject goHit = hit.transform.gameObject;
                    if (goHit.tag == "target")
                    {
                        score++;
                        print("score ++");
                        Destroy(goHit);
                        SpawnTarget();
                    }
                }
            }

            // ----- TIME -----
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0f)
            {
                EndGame();
            }
            else
            {
                mainPanel.UpdateTime(timeLeft);
            }
        }
    }

    public void StartGame(int duration)
    {
        time = duration;
        timeLeft = time;
        mainPanel.HideMenu();
        SpawnTarget();
        isPlaying = true;
    }

    private void SpawnTarget()
    {
        lastTarget = Instantiate(targetPrefab, Camera.main.transform);
        float randomWidth = Random.Range(-widthLimit, widthLimit);
        float randomHeight = Random.Range(-heightLimit, heightLimit - 200f); // Offset on the top of the screen (200px)
        lastTarget.transform.localPosition = new Vector3(randomWidth, randomHeight, 10);
    }

    private void EndGame()
    {
        isPlaying = false;
        Destroy(lastTarget);

        string bestScoreTime = "bestScore" + time;
        int bestScore = PlayerPrefs.GetInt(bestScoreTime, 0);

        if (bestScore < score)
        {
            bestScore = score;
            PlayerPrefs.SetInt(bestScoreTime, bestScore);
        }

        mainPanel.EndGame(score, bestScore);
    }

}
