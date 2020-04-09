using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonSceneController : MonoBehaviour
{
    // Initalize variables
    public GameObject balloonPrefab;
    public OVRHand leftHand;
    public OVRHand rightHand;
    public Text scoreText;
    public float maxX;
    public float maxY;
    public float maxZ;

    // Initialize private variables
    private List<GameObject> balloons;
    private float averageHandSpeed;
    private static float pathLength;
    private float time;
    private bool gameStarted;
    private static bool gameOver;
    private static bool showRes;
    private int score;
    private static Vector3 leftPos;
    private static Vector3 rightPos;
    private static float leftyDist;
    private static float rightyDist;
    private static string hand;

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        gameOver = false;
        showRes = false;

        leftyDist = 0.0f;
        rightyDist = 0.0f;
        pathLength = 0;
        score = 0;
        hand = "none";

        leftPos = leftHand.transform.position;
        rightPos = rightHand.transform.position;

        balloons = new List<GameObject>();
        CreateBalloon();
    }

    // Update is called once per frame
    void Update()
    {
        CreateBalloon();
        UpdateScreenText();
        TrackPathLength();
    }

    private void CreateBalloon()
    {
        if (!Balloon.IsLastActive() && balloons.Count < 12)
        {
            float x = Random.Range(-maxX, maxX);
            float y = Random.Range(0.2f, maxY);
            float z = Random.Range(0, maxZ);
            balloons.Add(Instantiate(balloonPrefab, new Vector3(x, y, z), Quaternion.identity));

            if (balloons.Count == 2 && !gameStarted)
            {
                gameStarted = true;
                time = Time.time;
            }
        }

        else if (balloons.Count == 12 && !Balloon.IsLastActive())
            EndGame();
    }

    void TrackPathLength()
    {
        leftyDist += Mathf.Sqrt(Mathf.Pow(leftHand.transform.position.x - leftPos.x, 2));
        leftyDist += Mathf.Sqrt(Mathf.Pow(leftHand.transform.position.y - leftPos.y, 2));
        leftyDist += Mathf.Sqrt(Mathf.Pow(leftHand.transform.position.z - leftPos.z, 2));
        leftPos = leftHand.transform.position;

        rightyDist += Mathf.Sqrt(Mathf.Pow(rightHand.transform.position.x - rightPos.x, 2));
        rightyDist += Mathf.Sqrt(Mathf.Pow(rightHand.transform.position.y - rightPos.y, 2));
        rightyDist += Mathf.Sqrt(Mathf.Pow(rightHand.transform.position.z - rightPos.z, 2));
        rightPos = rightHand.transform.position;
    }


    void UpdateScreenText()
    {
        if (!gameOver)
        {
            score = balloons.Count - 1;
            scoreText.text =
                "Time: " + (Time.time - time).ToString("f1") + "s" +
                "\nScore: " + score.ToString() +
                "\nhand: " + hand;
        }

        else if (showRes)
            DisplayResults();
    }

    public static void UpdatePathLength(string name, Vector3 pos)
    {
        Vector3 lastPos;
        float lastDist;

        if (name.Contains("_L"))
        {
            lastPos = leftPos;
            lastDist = leftyDist;
            hand = "left";
        }
        else
        { 
            lastPos = rightPos;
            lastDist = rightyDist;
            hand = "right";
        }

        lastDist += Mathf.Sqrt(Mathf.Pow(pos.x - lastPos.x, 2));
        lastDist += Mathf.Sqrt(Mathf.Pow(pos.y - lastPos.y, 2));
        lastDist += Mathf.Sqrt(Mathf.Pow(pos.z - lastPos.z, 2));
        pathLength += lastDist;

        leftyDist = 0.0f;
        rightyDist = 0.0f;
    }

    void DisplayResults()
    {
        averageHandSpeed = pathLength / (Time.time - time);
        scoreText.text =
            "Time: " + (Time.time - time).ToString("f1") + "s" +
            "\nScore: 12" +
            "\n Avg Hand Speed: " + averageHandSpeed.ToString("f2") +
            "\n Total Path Length: " + pathLength.ToString("f2");
        showRes = false;
        Time.timeScale = 0;
    }

    void EndGame()
    {
        gameOver = true;
        showRes = true;
    }

}
