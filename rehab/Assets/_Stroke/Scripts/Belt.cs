using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public GameObject belt;
    public Transform endPoint;
    public float speed;

    private void OnTriggerStay(Collider other)
    {
        other.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
    }
}
