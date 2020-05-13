using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInChildren<Rigidbody>().useGravity = false;
        other.GetComponentInChildren<Rigidbody>().isKinematic = true;
    }
}
