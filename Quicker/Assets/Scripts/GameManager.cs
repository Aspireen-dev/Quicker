using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GamePanel gamePanel;

    private int nbTargets; // Number of targets to hit, got with scene name : "10" - "25" - "50"
    private int nbTargetsLeft;

    private float time = 0f;
    private float maxTime;

    private bool isPlaying = false;

    void Start()
    {
        nbTargets = int.Parse(SceneManager.GetActiveScene().name);
        nbTargetsLeft = nbTargets;
        maxTime = nbTargets * 2;

        gamePanel.SetNewTargetPosition();
        isPlaying = true;
    }

    void Update()
    {
        // If we're not playing, do nothing
        if (!isPlaying)
        {
            return;
        }

        UpdateTime();
        //CheckForInput();
    }

    private void UpdateTime()
    {
        time += Time.deltaTime;
        if (time >= maxTime)
        {
            time = maxTime;
            EndGame();
        }
        gamePanel.UpdateTime(time);
    }

    public void TargetHit()
    {
        nbTargetsLeft--;

        if (nbTargetsLeft == 0)
        {
            EndGame();
            return;
        }

        gamePanel.SetNewTargetPosition();
    }

    private void EndGame()
    {
        isPlaying = false;
        gamePanel.EndGame(time, GetBestTimeSaved());
    }

    private float GetBestTimeSaved()
    {
        string bestTimeMode = "bestTime" + nbTargets;
        float bestTimeSaved = float.Parse(PlayerPrefs.GetString(bestTimeMode, maxTime.ToString("F")));

        if (bestTimeSaved > time)
        {
            bestTimeSaved = time;
            PlayerPrefs.SetString(bestTimeMode, time.ToString("F"));
        }

        return bestTimeSaved;
    }

    /*
    private void CheckForInput()
    {
        // ----- INPUTS -----
#if UNITY_EDITOR || UNITY_STANDALONE
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        Vector3 touchPositionOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID
        if (Input.touchCount == 0)
        {
            return;
        }
        Vector3 touchPositionOnScreen = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif

        Vector2 touchPosition = new Vector2(touchPositionOnScreen.x, touchPositionOnScreen.y);

        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "target")
            {
                TargetHit();
            }
        }
    }
    */

}
