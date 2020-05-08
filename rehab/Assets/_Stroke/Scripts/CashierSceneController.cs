using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashierSceneController : MonoBehaviour
{
    public int itemsToSpawn;
    public Text scoreText;
    public GameObject belt;
    public GameObject fruitBox;
    public GameObject meatBox;
    public GameObject dairyBox;

    public List<Item> fruits;
    public List<Item> veggies;
    public List<Item> meats;


    private List<Vector3> startPos;
    private List<int> itemFreq;
    private float itemTimer;
    private List<Item> itemsSpawned;
    private int itemsDropped;
    private int score;
    private int fails;
    


    // Start is called before the first frame update
    void Start()
    {
        RandomizeBagOrder();
        itemsSpawned = new List<Item>();
        itemFreq = new List<int>() { 4, 4, 4 };
        itemTimer = Time.time;
        itemsDropped = 0;
        score = 0;
        SpawnRandomItem();
        scoreText.text = " ";
        fails = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // If the last item has dropped or has been placed in a box, end the game
        if (itemsSpawned.Count == itemsToSpawn &&
            (itemsSpawned[itemsSpawned.Count - 1].IsInBox() ||
            itemsSpawned[itemsSpawned.Count - 1].transform.position.y < 0) &&
            Time.timeScale > 0.0f)
        {
            DisplayResults();
            Time.timeScale = 0.0f;
        }

        // Timer for spawning items
        if (Time.time - itemTimer > 1.0f)
        {
            SpawnRandomItem();
            itemTimer = Time.time;
        }
        
    }

    void DisplayResults()
    {
        belt.SetActive(false);
        foreach (Item item in itemsSpawned)
        {
            if (item.IsInCorrectBox())
                score++;

            else if (item.WasMissed())
                itemsDropped++;

            else
                fails++;
        }

        fails += itemsDropped;
        scoreText.text = "Score: " + score.ToString() + ", Fails: " + fails.ToString() +
            "\nItems dropped: " + itemsDropped + ", Accuracy " + (score/itemsToSpawn).ToString();
    }

    void RandomizeBagOrder()
    {
        startPos = new List<Vector3>()
        {
            fruitBox.transform.position,
            meatBox.transform.position,
            dairyBox.transform.position
        };

        for (int i = 0; i < startPos.Count; i++)
        {
            Vector3 temp = startPos[i];
            int swap = Random.Range(i, startPos.Count);
            startPos[i] = startPos[swap];
            startPos[swap] = temp;
        }

        fruitBox.transform.position = startPos[0];
        meatBox.transform.position = startPos[1];
        dairyBox.transform.position = startPos[2];
    }

    void Spawn(List<Item> item, int index)
    {
        itemsSpawned.Add(Instantiate(item[index], new Vector3(0.06f, 1.01f, 2.15f), Quaternion.identity));
    }

    void SpawnRandomItem()
    {
        if (itemsSpawned.Count == 12)
            return;

        // Pick random item category to spawn
        int index = Random.Range(0, 3);
        while (itemFreq[index] == 0)
            index = Random.Range(0, 3);

        // Decrease amount of times left to spawn type of item
        itemFreq[index]--;

        // Spawn item
        switch (index)
        {
            case 0: // going to spawn a random fruit
                index = Random.Range(0, 4);
                Spawn(fruits, index);
                break;

            case 1: // going to spawn a random veggie
                index = Random.Range(0, 3);
                Spawn(veggies, index);
                break;

            case 2: // going to spawn a random veggie
                index = Random.Range(0, 2);
                Spawn(meats, index);
                break;
        }
    }


}
