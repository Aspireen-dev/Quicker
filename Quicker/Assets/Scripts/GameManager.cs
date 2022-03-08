using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject target; // Size of target : 150px * 150px
    [SerializeField]
    private MainPanel mainPanel;

    // Default values for 1080 * 1920 --- width and height divided by 2 and minus 100
    private float widthLimit;
    private float heightLimit;
    private float topOffset = 300f;

    private bool isPlaying = false;

    private int nbTargets;
    private int nbTargetsLeft;
    private float time;
    private float maxTime = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Rect screen = GameObject.Find("Canvas").GetComponent<RectTransform>().rect;
        widthLimit = (screen.width / 2) - 150f;
        heightLimit = (screen.height / 2) - 150f;
    }

    // Update is called once per frame
    void Update()
    {
        // If we're not playing, do nothing
        if (!isPlaying)
        {
            return;
        }

        // ----- TIME -----
        time += Time.deltaTime;
        if (time >= maxTime)
        {
            time = maxTime;
            EndGame();
        }
        else
        {
            mainPanel.UpdateTime(time);
        }

        /*
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
        */
    }

    public void StartGame(int targets)
    {
        mainPanel.HideMenu();

        nbTargets = targets;
        nbTargetsLeft = nbTargets;

        time = 0;

        target.SetActive(true);
        SetNewTargetPosition();

        isPlaying = true;
    }

    public void TargetHit()
    {
        nbTargetsLeft--;

        if (nbTargetsLeft == 0)
        {
            EndGame();
            return;
        }

        SetNewTargetPosition();
    }

    private void SetNewTargetPosition()
    {
        float randomWidth = Random.Range(-widthLimit, widthLimit);
        float randomHeight = Random.Range(-heightLimit, heightLimit - topOffset); // Offset on the top of the screen (300px)
        target.transform.localPosition = new Vector3(randomWidth, randomHeight, 0);
    }

    private void EndGame()
    {
        isPlaying = false;
        target.SetActive(false);

        mainPanel.EndGame(time, GetBestTimeSaved());
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

}
