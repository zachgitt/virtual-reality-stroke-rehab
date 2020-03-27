using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Initalize variables
    public GameObject balloonPrefab;

    // Initialize private variables
    private List<GameObject> balloons;

    // Start is called before the first frame update
    void Start()
    {
        balloons = new List<GameObject>();
        balloons.Add(Instantiate(balloonPrefab, new Vector3(1, 0.5f, 0), Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
