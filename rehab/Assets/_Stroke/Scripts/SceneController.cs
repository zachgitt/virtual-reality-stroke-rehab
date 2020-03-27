using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Initalize variables
    public GameObject balloonPrefab;
    public float maxX;
    public float maxY;
    public float maxZ;

    // Initialize private variables
    private GameObject balloon;

    // Start is called before the first frame update
    void Start()
    {
        balloon = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Instantiate a balloon
        if (balloon == null || balloon.Equals(null))
        {
            float x = Random.Range(-maxX, maxX);
            float y = Random.Range(0, maxY);
            float z = Random.Range(-maxZ, maxZ);
            balloon = Instantiate(balloonPrefab, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
