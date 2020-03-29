using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSceneController : MonoBehaviour
{
    // Initalize variables
    public GameObject balloonPrefab;
    public float maxX;
    public float maxY;
    public float maxZ;

    // Initialize private variables
    private List<GameObject> balloons;

    // Start is called before the first frame update
    void Start()
    {
        balloons = new List<GameObject>();
        balloons.Add(CreateBalloon());
    }

    // Update is called once per frame
    void Update()
    {
        // Instantiate a balloon
        if (!Balloon.IsLastActive())
        {
            balloons.Add(CreateBalloon());
        }
    }

    private GameObject CreateBalloon()
    {
        float x = Random.Range(-maxX, maxX);
        float y = Random.Range(0.2f, maxY);
        float z = Random.Range(-maxZ, maxZ);
        return Instantiate(balloonPrefab, new Vector3(x, y, z), Quaternion.identity);
    }
}
