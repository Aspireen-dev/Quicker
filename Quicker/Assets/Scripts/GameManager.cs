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

    private int nbTargets;
    private int nbTargetsLeft;
    private float time;
    private float maxTime = 100f;

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
            GameObject goHit = hit.transform.gameObject;
            if (goHit.tag == "target")
            {
                nbTargetsLeft--;
                print("nbTargets --");
                Destroy(goHit);
                if (nbTargetsLeft > 0)
                {
                    SpawnTarget();
                }
                else
                {
                    EndGame();
                    return;
                }
            }
        }
    }

    public void StartGame(int targets)
    {
        nbTargets = targets;
        nbTargetsLeft = nbTargets;
        time = 0;
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

        string bestTimeMode = "bestTime" + nbTargets;
        float bestTimeSaved = float.Parse(PlayerPrefs.GetString(bestTimeMode, maxTime.ToString("F")));

        if (bestTimeSaved > time)
        {
            bestTimeSaved = time;
            PlayerPrefs.SetString(bestTimeMode, time.ToString("F"));
        }

        mainPanel.EndGame(time, bestTimeSaved);
    }

}
