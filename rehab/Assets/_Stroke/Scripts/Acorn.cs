using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : OVRGrabbable
{
    public Material startingMaterial;

    private int interactions;
    private bool inBasket;
    private Vector3 startPos;
    private float pathLength = 0.0f;
    private Vector3 prevPos;

    // Start is called before the first frame update
    protected override void Start()
    {
        startingMaterial = GetComponentInChildren<MeshRenderer>().material;
        startPos = transform.position;
        prevPos = startPos;
        inBasket = false;
        pathLength = 0.0f;
        interactions = 0;
        base.Start();
    }

    private void Update()
    {
        UpdatePathLength();
        CheckPos();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basket"))
            inBasket = true;
    }


    void UpdatePathLength()
    {
        if (!inBasket)
        {
            pathLength += Mathf.Sqrt(Mathf.Pow(prevPos.x - transform.position.x, 2));
            pathLength += Mathf.Sqrt(Mathf.Pow(prevPos.y - transform.position.y, 2));
            pathLength += Mathf.Sqrt(Mathf.Pow(prevPos.z - transform.position.z, 2));
            prevPos = transform.position;
        }
    }

    public bool IsInBasket() { return inBasket; }
    public float GetPathLength() { return pathLength; }
    public int GetInteractions() { return interactions; }

    void CheckPos()
    {
        /* If acorn is under map and not in the basket, reset acorn position */
        if (transform.position.y <= 0.0f & !inBasket)
        {
            GetComponentInChildren<Rigidbody>().useGravity = false;
            GetComponentInChildren<Rigidbody>().isKinematic = true;
            transform.position = startPos;
            pathLength = 0.0f;
        }
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        GetComponentInChildren<Renderer>().material.color = startingMaterial.color;
        interactions++;
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        GetComponentInChildren<Rigidbody>().useGravity = true;
        GetComponentInChildren<Rigidbody>().isKinematic = false;
    }

}
