using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketSceneController : MonoBehaviour
{

    // Initalize variables
    public GameObject basketPrefab;
    public GameObject squirrelPrefab;
    public GameObject acornPrefab;
    public bool showSquirrel;
    public bool basketInUpperHalf;

    public float maxX;
    public float maxY;
    public float maxZ;

    // Initialize private variables
    private List<GameObject> acorns;
    private GameObject basket;
    private GameObject squirrel;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 basketPos = new Vector3(0, 1, 0.5f); ;
        if (!basketInUpperHalf)
        {
            basketPos -= new Vector3(0, 1, 0.25f);
        }

        basket = Instantiate(basketPrefab, basketPos, Quaternion.identity);

        if (showSquirrel)
        {
            squirrel = Instantiate(squirrelPrefab, basket.transform.position, Quaternion.identity);
            squirrel.transform.position -= new Vector3(0, 0, 0.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
