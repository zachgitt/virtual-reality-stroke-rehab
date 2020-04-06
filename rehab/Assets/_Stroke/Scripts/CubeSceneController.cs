using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRHand;

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
    public Material outlineMaterial;
    public OVRHand leftHand;
    public OVRHand rightHand;

    // Declare private variables
    private List<GameObject> solidCubes;
    private List<GameObject> hollowCubes;
    private int completed;
    private Color currColor;

    // Start is called before the first frame update
    void Start()
    {
        solidCubes = new List<GameObject>();
        hollowCubes = new List<GameObject>();
        completed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Declare temp variables
        int last = solidCubes.Count - 1;

        // Create new pair
        if (completed < numCubes)
        {
            TryAppendSolidHollow();
        }

        // Pinch solid cube
        if (GrabbingAndPinching(solidCubes[last], 0.5f))
        {
            PickupPrefab(solidCubes[last]);
        }
        else
        {
            DropPrefab(solidCubes[last]);
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

    private void TryAppendSolidHollow()
    {
        if (solidCubes.Count == 0 /*|| !solidCubes[solidCubes.Count-1].activeInHierarchy*/)
        {
            // Initialize parameters
            float size = Random.Range(minSize, maxSize);
            float x = Random.Range(-maxX, maxX);
            float y = Random.Range(0.5f, maxY);
            float z = Random.Range(0.0f, maxZ);
            currColor = Random.ColorHSV();

            // Create solid cube
            //GameObject solidCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //solidCube.transform.position = new Vector3(x, y, z);
            //solidCube.transform.localScale = new Vector3(size, size, size);
            //solidCube.GetComponent<Renderer>().material.color = currColor;
            //solidCube.AddComponent<Rigidbody>();
            //solidCube.GetComponent<Rigidbody>().isKinematic = true;
            //solidCube.AddComponent<BoxCollider>();
            //solidCube.GetComponent<BoxCollider>().isTrigger = true;
            //solidCube.AddComponent<OVRGrabbable>();
            GameObject solidCube = Instantiate(solidCubePrefab, new Vector3(x, y, z), Quaternion.identity);
            solidCube.transform.localScale = new Vector3(size, size, size);
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
        }
    }

    private void PickupPrefab(GameObject prefab)
    {
        // Highlight prefab
        prefab.GetComponent<Renderer>().material = outlineMaterial;
        prefab.GetComponent<Renderer>().material.color = currColor;
    }

    private void DropPrefab(GameObject prefab)
    {
        // Unhighlight prefab
        prefab.GetComponent<Renderer>().material = new Material(Shader.Find("Diffuse"));
        prefab.GetComponent<Renderer>().material.color = currColor;
    }
}
