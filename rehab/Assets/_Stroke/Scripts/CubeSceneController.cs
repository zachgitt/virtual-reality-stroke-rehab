using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRHand;
using UnityEngine.UI;


public class CubeSceneController : MonoBehaviour
{
    // Declare public variables
    public GameObject hollowCubePrefab;
    public GameObject solidCubePrefab;
    public uint numCubes;
    public float minDistance;
    public float maxDistance;
    public float maxX;
    public float maxY;
    public float maxZ;
    public float minSize;
    public float maxSize;
    public float hollowToSolidScale;
    public Material outlineMaterial;
    public OVRHand leftHand;
    public OVRHand rightHand;

    // Declare private variables
    private List<GameObject> solidCubes;
    private List<GameObject> hollowCubes;
    private Color currColor;
    private float startTime;
    private Text scoreText;
    private bool createCubes;
    private float pinchThresh;
    private List<float> timers;
    private List<float> distances;
    private float cubeStart;
    private bool gameOver;
    private float endTime;
    private float avgHandSpeed;

    // Start is called before the first frame update
    void Start()
    {
        solidCubes = new List<GameObject>();
        hollowCubes = new List<GameObject>();
        timers = new List<float>();
        distances = new List<float>();
        startTime = -1;
        scoreText = GetComponentInChildren<Text>();
        createCubes = true;
        pinchThresh = 0.5f;
        cubeStart = -1;
        gameOver = false;
        avgHandSpeed = -1;
    }

    // Update is called once per frame
    void Update()
    {
        // Create new pair
        UpdateSolidHollow();
        MovePrefab(solidCubes[solidCubes.Count - 1], pinchThresh);
        UpdateCanvas();
    }

    private void MovePrefab(GameObject prefab, float pinchThresh)
    {
        // Pinch solid cube
        if (GrabbingAndPinching(prefab, pinchThresh))
        {
            PickupPrefab(prefab);
        }
        else
        {
            DropPrefab(prefab);
        }
    }

    private void UpdateCanvas()
    {

        // Gameover
        if (gameOver)
        {
           
            scoreText.text = "Score: " + (solidCubes.Count - 1).ToString() + "\n"
                           + "Time: " + (endTime - startTime).ToString("f1") + "\n"
                           + "Avg Hand Speed: " + avgHandSpeed.ToString("f2");
        }
        else
        {
            // Update canvas during game
            scoreText.text = "Score: " + (solidCubes.Count - 1).ToString() + "\n"
                            + "Time: " + (Time.time - startTime).ToString("f1");
        }
    }

    private bool GrabbingAndPinching(GameObject cube, float thresh)
    {
        float leftPinchStrength = leftHand.GetFingerPinchStrength(HandFinger.Index);
        float rightPinchStrength = rightHand.GetFingerPinchStrength(HandFinger.Index);
        bool grabbed = cube.GetComponent<OVRGrabbable>().isGrabbed;

        if ((leftPinchStrength > thresh && grabbed) || (rightPinchStrength > thresh && grabbed))
        {
            return true;
        }
        return false;
    }

    private void UpdateSolidHollow()
    {

        if (createCubes && solidCubes.Count < numCubes)
        {
            // Create 1 pair at a time
            createCubes = false;

            // Initialize parameters
            float size = Random.Range(minSize, maxSize);
            float x = Random.Range(-maxX, maxX);
            float y = Random.Range(0.5f, maxY);
            float z = Random.Range(0.0f, maxZ);
            currColor = Random.ColorHSV();

            // Create solid cube
            float solidSize = size * hollowToSolidScale;
            GameObject solidCube = Instantiate(solidCubePrefab, new Vector3(x, y, z), Quaternion.identity);
            solidCube.transform.localScale = new Vector3(solidSize, solidSize, solidSize);
            solidCube.GetComponent<Renderer>().material.color = currColor; // TODO: remove this or MeshRenderer
            solidCube.GetComponent<MeshRenderer>().material.color = currColor;
            solidCubes.Add(solidCube);

            // Create hollow cube
            float x_ = Random.Range(x + minDistance, x + maxDistance);
            float y_ = Random.Range(y + minDistance, y + maxDistance);
            float z_ = Random.Range(z + minDistance, z + maxDistance);
            GameObject hollowCube = Instantiate(hollowCubePrefab, new Vector3(x_, y_, z_), Quaternion.identity);
            hollowCube.transform.localScale = new Vector3(size, size, size);
            // Recolor the entire frame
            foreach (Transform child in hollowCube.transform)
            {
                child.GetComponent<Renderer>().material.color = currColor;
            }
            hollowCubes.Add(hollowCube);

            // Save cube distance
            float dx = x - x_;
            float dy = y - y_;
            float dz = z - z_;
            float distance = (float) System.Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
            distances.Add(distance);
        }
    }

    private void PickupPrefab(GameObject prefab)
    {
        // Start the game
        if (startTime == -1)
        {
            startTime = Time.time;
        }

        // Start the cube timer
        if (cubeStart == -1)
        {
            cubeStart = Time.time;
        }

        // Highlight prefab
        prefab.GetComponent<Renderer>().material = outlineMaterial;
        prefab.GetComponent<Renderer>().material.color = currColor;
    }

    private void DropPrefab(GameObject prefab)
    {
        // Unhighlight prefab
        prefab.GetComponent<Renderer>().material = new Material(Shader.Find("Diffuse"));
        prefab.GetComponent<Renderer>().material.color = currColor;

        // End the cube timer
        if (SolidInsideHollow(prefab, hollowCubes[hollowCubes.Count-1]))
        {
            float elapsedTime = Time.time - cubeStart;
            timers.Add(elapsedTime);
            createCubes = true;
            prefab.SetActive(false);
            cubeStart = -1;
            hollowCubes[hollowCubes.Count - 1].SetActive(false);
        }


        // End the game
        if (timers.Count == numCubes)
        {
            gameOver = true;
            endTime = Time.time;
            float totalDistance = 0;
            float totalTime = 0;
            foreach (float d in distances) totalDistance += d;
            foreach (float t in timers) totalTime += t;
            avgHandSpeed = totalDistance / totalTime;
        }
    }

    private bool SolidInsideHollow(GameObject solid, GameObject hollow)
    {
        // Save 8 corner combinations of (1,1,1)...(-1,-1,-1)
        List<Vector3> corners = new List<Vector3>();
        for (int x = -1; x < 2; x += 2)
        {
            for (int y = -1; y < 2; y += 2)
            {
                for (int z = -1; z < 2; z += 2)
                {
                    Vector3 coords = solid.transform.TransformPoint(new Vector3(x, y, z));
                    corners.Add(coords);
                }
            }
        }

        // Save boundaries
        float xmin = hollow.transform.TransformPoint(new Vector3(-1, 0, 0)).x;
        float xmax = hollow.transform.TransformPoint(new Vector3(1, 0, 0)).x;
        float ymin = hollow.transform.TransformPoint(new Vector3(0, -1, 0)).y;
        float ymax = hollow.transform.TransformPoint(new Vector3(0, 1, 0)).y;
        float zmin = hollow.transform.TransformPoint(new Vector3(0, 0, -1)).z;
        float zmax = hollow.transform.TransformPoint(new Vector3(0, 0, 1)).z;

        // Check corners out of bounds
        foreach (Vector3 corner in corners)
        {
            if (corner.x <= xmin || xmax <= corner.x ||
                corner.y <= ymin || ymax <= corner.y ||
                corner.z <= zmin || zmax <= corner.z)
            {
                return false;
            }
        }


        return true;
    }
}
