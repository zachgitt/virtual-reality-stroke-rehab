using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketSceneController : MonoBehaviour
{
    // Initalize publicvariables
    public GameObject basketPrefab;
    public GameObject squirrelPrefab;
    public GameObject acornPrefab;
    public bool showSquirrel;
    public bool basketInUpperHalf;
    public float maxX;
    public float maxY;
    public float maxZ;

    // Initialize private variables
    private GameObject basket;
    private GameObject squirrel;

    //Initiate static variables
    public static List<GameObject> acorns;
    public static bool addAcorn;
    public static float acornsNotInBasket;
    private static Text scoreText;
    private static int interactions;
    private static float time;
    private static bool gameOver;
    private static float totalPathLength;
    private static float accuracy;
    private static float handSpeed;


    // Start is called before the first frame update
    void Start()
    {
        basket = Instantiate(basketPrefab, new Vector3(0, 0.6f, 0.25f), Quaternion.identity);
        if (!basketInUpperHalf)
            basket.transform.position -= new Vector3(0, 0.3f, 0);
        acorns = new List<GameObject>();
        addAcorn = true;
        gameOver = false;
        interactions = 0;
        acornsNotInBasket = 0;
        time = Time.time;
        scoreText = GetComponentInChildren<Text>();
        AddAcorn();
        MakeSquirrel();
    }

    // Update is called once per frame
    void Update()
    {
        MakeSquirrel();
        AddAcorn();
        if (!gameOver)
        {
            totalPathLength = Acorn.GetPathLength();
            scoreText.text = "Score: " + (acorns.Count - 1).ToString() +
                "\nTime: " + (Time.time - time).ToString("f1");
        }
    }

    void MakeSquirrel()
    {
        if(showSquirrel)
        {
            squirrel = Instantiate(squirrelPrefab, basket.transform.position, Quaternion.identity);
            squirrel.transform.position -= new Vector3(0, 0, 0.3f);
        }
    }

    void AddAcorn()
    {
        if (addAcorn && acorns.Count < 12 && !gameOver)
        {
            addAcorn = false;
            float x = UnityEngine.Random.Range(-maxX, maxX);
            float y = UnityEngine.Random.Range(0.2f, maxY);
            float z = UnityEngine.Random.Range(0, maxZ);
            acorns.Add(Instantiate(acornPrefab, new Vector3(x, y, z), Quaternion.identity));
        }

    }

    public static void AddInteraction()
    {
        interactions++;
    }

    public static void StartGameTimer()
    {
        time = Time.time;
    }

    public static void EndGame()
    {
        gameOver = true;
        //Update screen text to display all measurements
        time = Time.time - time;
        accuracy = (12.0f / (float)interactions) * 100.0f;
        handSpeed = totalPathLength / time;
        scoreText.text = "Score: " + (acorns.Count).ToString() +
            ", Time: " + time.ToString("f1") +
            "\nTotal Path Length: " + totalPathLength.ToString("f3") +
            "\nAccuracy: " + accuracy.ToString("f1") +
            "%\nAvg hand speed: " + handSpeed.ToString("f2");
    }

}
