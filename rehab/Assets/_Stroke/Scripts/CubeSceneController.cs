using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRHand;

public class CubeSceneController : MonoBehaviour
{
    // Declare public variables
    public GameObject hollowCubePrefab;
    public uint numCubes;
    public float minDistance;
    public float maxDistance;
    public float maxX;
    public float maxY;
    public float maxZ;
    public float minSize;
    public float maxSize;

    // Declare private variables
    private List<GameObject> solidCubes;
    private List<GameObject> hollowCubes;
    private int completed;

    // Start is called before the first frame update
    void Start()
    {
        solidCubes = new List<GameObject>();
        completed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Create new pair
        if (completed < numCubes)
        {
            TryAppendSolidHollow();
        }

        // Playing game
        var hand = GetComponent<OVRHand>();
        if (hand.GetFingerIsPinching(HandFinger.Index))
        {
            Debug.Log("");
        }
    }

    private void TryAppendSolidHollow()
    {
        if (solidCubes.Count == 0 /*|| !solidCubes[solidCubes.Count-1].activeInHierarchy*/)
        {
            // Initialize parameters
            float size = Random.Range(minSize, maxSize);
            float x = Random.Range(-maxX, maxX);
            float y = Random.Range(0.5f, maxY);
            float z = Random.Range(0.0f, maxZ);
            Color c = Random.ColorHSV();

            // Create solid cube
            GameObject solidCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            solidCube.transform.position = new Vector3(x, y, z);
            solidCube.transform.localScale = new Vector3(size, size, size);
            solidCube.GetComponent<Renderer>().material.color = c;
            solidCubes.Add(solidCube);

            // Create hollow cube
            float x_ = Random.Range(x + minDistance, x + maxDistance);
            float y_ = Random.Range(y + minDistance, y + maxDistance);
            float z_ = Random.Range(z + minDistance, z + maxDistance);
            GameObject hollowCube = Instantiate(hollowCubePrefab, new Vector3(x_, y_, z_), Quaternion.identity);
            hollowCube.transform.localScale = new Vector3(size, size, size);
            foreach (Transform child in hollowCube.transform)
            {
                child.GetComponent<Renderer>().material.color = c;
            }
            hollowCubes.Add(hollowCube);
        }
    }
}
