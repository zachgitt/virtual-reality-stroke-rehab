using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private static int numBalloons = 0;

    // Start is called before the first frame update
    void Start()
    {
        SphereCollider sphere = gameObject.GetComponentInChildren<SphereCollider>();
        sphere.center = new Vector3(-.67f, 0.87f, -0.1f);
        sphere.radius = 0.24f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.LogFormat("%d", numBalloons);
        if (++numBalloons % 2 == 0)
            gameObject.transform.position -= new Vector3(1, 0, 0);

        else
            gameObject.transform.position += new Vector3(1, 0, 0);

        if (numBalloons == 6)
        {
            //gameObject.SetActive(false);
            gameObject.transform.position += new Vector3(0, 0, 0.6f);
        }
    }
}
