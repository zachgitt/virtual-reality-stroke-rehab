using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketSceneController : MonoBehaviour
{
    // Initalize variables
    public GameObject basketPrefab;
    public GameObject squirrelPrefab;
    public GameObject acornPrefab;
    public Text scoreText;
    public bool showSquirrel;
    public bool basketInUpperHalf;
    public float maxX;
    public float maxY;
    public float maxZ;
    public static bool addAcorn;
    public static float acornsNotInBasket;

    // Initialize private variables
    public static List<GameObject> acorns;
    private GameObject basket;
    private GameObject squirrel;
    private float totalPathLength;
    private bool gameOver;


    // Start is called before the first frame update
    void Start()
    {
        basket = Instantiate(basketPrefab, new Vector3(0, 0.6f, 0.25f), Quaternion.identity);
        if (!basketInUpperHalf)
            basket.transform.position -= new Vector3(0, 0.3f, 0);
        acorns = new List<GameObject>();
        addAcorn = true;
        gameOver = false; 
        AddAcorn();
        MakeSquirrel();
    }

    // Update is called once per frame
    void Update()
    {
        MakeSquirrel();
        AddAcorn();
        if (!gameOver)
            totalPathLength = Acorn.GetPathLength();

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

        if (addAcorn && acorns.Count < 12 && acornsNotInBasket < 1)
        {
            addAcorn = false;
            float x = UnityEngine.Random.Range(-maxX, maxX);
            float y = UnityEngine.Random.Range(0.1f, maxY);
            float z = UnityEngine.Random.Range(0, maxZ);
            acorns.Add(Instantiate(acornPrefab, new Vector3(x, y, z), Quaternion.identity));
            scoreText.text = "Score: " + (acorns.Count - 1).ToString() + "\nTotal Path Length: " + totalPathLength.ToString("f3");
        }

        else if (acorns.Count == 12 && !gameOver)
        {
            gameOver = true;
            scoreText.text = "Score: " + (acorns.Count).ToString() + "\nTotal Path Length: " + totalPathLength.ToString();
        }

    }
}
