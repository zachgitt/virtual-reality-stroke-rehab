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
    public Acorn acornPrefab;
    public float maxX;
    public float maxY;
    public float maxZ;
    public Text scoreText;

    // Initialize private variables
    private GameObject basket;
    private GameObject squirrel;
    private List<Acorn> acorns;
    private float time;
    private bool gameOver;
    private bool gameStarted;


    // Start is called before the first frame update
    void Start()
    {
        acorns = new List<Acorn>();
        time = 0.0f;
        gameOver = false;
        gameStarted = false;

        BasketInit();
        MakeSquirrel();
        SpawnAcorn();
    }

    // Update is called once per frame
    void Update()
    {
        if (acorns[0].GetInteractions() > 0 && !gameStarted)
        {
            time = Time.time;
            gameStarted = true;
        }

        if (acorns.Count < 12 && acorns[acorns.Count - 1].IsInBasket())
            SpawnAcorn();

        if (!gameOver)
            scoreText.text = "Score: " + (acorns.Count - 1).ToString() +
                    "\nTime: " + (Time.time - time).ToString("f1");

        if (acorns.Count == 12)
        {
            if (acorns[11].IsInBasket())
            {
                EndGame();
                gameOver = true;
                Time.timeScale = 0.0f;
            }
        }
    }

    void MakeSquirrel()
    {
        int show = UnityEngine.Random.Range(0, 51);
        if(show > 25)
        {
            squirrel = Instantiate(squirrelPrefab, basket.transform.position, Quaternion.identity);
            squirrel.transform.position -= new Vector3(0, 0, 0.2f);
        }
    }

    void SpawnAcorn()
    {
        float x = UnityEngine.Random.Range(-maxX, maxX);
        float y = UnityEngine.Random.Range(basket.transform.position.y, maxY);
        float z = UnityEngine.Random.Range(0, maxZ);
        acorns.Add(Instantiate(acornPrefab, new Vector3(x, y, z), Quaternion.identity));
    }

    private void BasketInit()
    {
        basket = Instantiate(basketPrefab, new Vector3(0, 0.6f, 0.25f), Quaternion.identity);
        int basketInUpperHalf = UnityEngine.Random.Range(0, 51);
        if (basketInUpperHalf > 25)
            basket.transform.position -= new Vector3(0, 0.3f, 0);
    }

    public void EndGame()
    {
        if (gameOver)
            return;

        int interactions = 0;
        float totalPathLength = 0.0f;
        float acc;
        float handSpeed;

        time = Time.time - time;
        foreach (Acorn ac in acorns)
        {
            interactions += ac.GetInteractions();
            totalPathLength += ac.GetPathLength();
        }

        acc = (12.0f / interactions) * 100.0f;
        handSpeed = totalPathLength / time;


        //Update screen text to display all measurements
        scoreText.text = "Score: " + (acorns.Count).ToString() +
            ", Time: " + time.ToString("f1") +
            "\nTotal Path Length: " + totalPathLength.ToString("f3") +
            "\nAccuracy: " + acc.ToString("f1") +
            "%\nAvg hand speed: " + handSpeed.ToString("f2");
        Time.timeScale = 0.0f;
    }

}
