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
    public float maxX;
    public float maxY;
    public float maxZ;
    public Text scoreText;

    // Initialize private variables
    private GameObject basket;
    private GameObject squirrel;
    private int i;

    //Initiate static variables
    private List<GameObject> acorns;
    private float time;


    // Start is called before the first frame update
    void Start()
    {
        BasketInit();
        acorns = new List<GameObject>();
        MakeSquirrel();
        i = 0;


        float x = UnityEngine.Random.Range(-maxX, maxX);
        float y = UnityEngine.Random.Range(0.2f, maxY);
        float z = UnityEngine.Random.Range(0, maxZ);
        acorns.Add(Instantiate(acornPrefab, new Vector3(x, y, z), Quaternion.identity));
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        AddAcorn();

        scoreText.text = "Score: " + (acorns.Count - 1).ToString() +
                "\nTime: " + (Time.time - time).ToString("f1") +
                "\ni: " + i++;
        i %= 100;
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

    void AddAcorn()
    {
        Acorn lastAcorn = acorns[acorns.Count - 1].GetComponentInChildren<Acorn>();
        if (acorns.Count < 12 && lastAcorn.IsInBasket())
        {
            float x = UnityEngine.Random.Range(-maxX, maxX);
            float y = UnityEngine.Random.Range(0.2f, maxY);
            float z = UnityEngine.Random.Range(0, maxZ);
            acorns.Add(Instantiate(acornPrefab, new Vector3(x, y, z), Quaternion.identity));
        }

        else if (acorns.Count == 12 && lastAcorn.IsInBasket())
            EndGame();

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
        int interactions = 0;
        float totalPathLength = 0.0f;
        float acc;
        float handSpeed;
        Acorn tempAcorn;

        time = Time.time - time;

        foreach (GameObject ac in acorns)
        {
            tempAcorn = ac.GetComponentInChildren<Acorn>();
            interactions += tempAcorn.GetInteractions();
            totalPathLength += tempAcorn.GetPathLength();
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
