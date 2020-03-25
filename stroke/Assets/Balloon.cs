using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("Hello");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        UnityEngine.Debug.Log("Yooooooooo");
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        UnityEngine.Debug.Log("Triggered");
    }
}
