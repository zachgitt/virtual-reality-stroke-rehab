using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonSceneController : MonoBehaviour
{
    // Initalize variables
    public Balloon balloonPrefab;
    public OVRHand leftHand;
    public OVRHand rightHand;
    public Text scoreText;
    public float maxX;
    public float maxY;
    public float maxZ;

    // Initialize private variables
    private List<Balloon> balloons;
    private float averageHandSpeed;
    private float pathLength;
    private float time;
    private bool gameOver;
    private Vector3 leftPos;
    private Vector3 rightPos;
    private float leftyDist;
    private float rightyDist;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;

        leftyDist = 0.0f;
        rightyDist = 0.0f;
        pathLength = 0;

        leftPos = leftHand.transform.position;
        rightPos = rightHand.transform.position;
        time = Time.time;


        balloons = new List<Balloon>();
        SpawnBalloon();
    }

    // Update is called once per frame
    void Update()
    {
        CreateBalloon();
        UpdateScreenText();
        TrackPathLength();

        if (balloons.Count == 12)
        {
            if (balloons[11].IsPopped())
            {
                UpdatePathLength(balloons[balloons.Count - 1].PoppedBy());
                DisplayResults();
                Time.timeScale = 0.0f;
            }
        }
    }

    void SpawnBalloon()
    {
        float x = Random.Range(-maxX, maxX);
        float y = Random.Range(0.2f, maxY);
        float z = Random.Range(0, maxZ);
        balloons.Add(Instantiate(balloonPrefab, new Vector3(x, y, z), Quaternion.identity));
    }

    private void CreateBalloon()
    {
        if (balloons[balloons.Count - 1].IsPopped() && balloons.Count < 12)
        {
            UpdatePathLength(balloons[balloons.Count - 1].PoppedBy());
            SpawnBalloon();
        }

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
            scoreText.text =
                "Time: " + (Time.time - time).ToString("f1") + "s" +
                "\nScore: " + (balloons.Count-1).ToString();
        }

        else
            DisplayResults();
    }

    void UpdatePathLength(string name)
    {
        Vector3 lastPos;
        Vector3 currHandPos;
        float lastDist;

        if (name.Contains("_L"))
        {
            currHandPos = leftHand.transform.position;
            lastPos = leftPos;
            lastDist = leftyDist;
        }
        else
        {
            currHandPos = rightHand.transform.position;
            lastPos = rightPos;
            lastDist = rightyDist;
        }

        lastDist += Mathf.Sqrt(Mathf.Pow(currHandPos.x - lastPos.x, 2));
        lastDist += Mathf.Sqrt(Mathf.Pow(currHandPos.y - lastPos.y, 2));
        lastDist += Mathf.Sqrt(Mathf.Pow(currHandPos.z - lastPos.z, 2));
        pathLength += lastDist;

        leftyDist = 0.0f;
        rightyDist = 0.0f;
    }

    void DisplayResults()
    {
        if (gameOver)
            return;

        averageHandSpeed = pathLength / (Time.time - time);
        scoreText.text =
            "Time: " + (Time.time - time).ToString("f1") + "s" +
            "\nScore: " + balloons.Count.ToString() +
            "\n Avg Hand Speed: " + averageHandSpeed.ToString("f2") +
            "\n Total Path Length: " + pathLength.ToString("f2");
        gameOver = true;
    }


}
