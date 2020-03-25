using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("Hellooooo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        UnityEngine.Debug.Log("Yaaaaaa");
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        UnityEngine.Debug.Log("Triggered2");
    }

}
