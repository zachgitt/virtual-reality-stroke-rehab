using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : OVRGrabbable
{
    public Material startingMaterial;

    private float tempPath;
    private bool inBasket;
    private Vector3 startPos;
    
    private static float pathLength = 0.0f;
    private bool gameOver;
    private Vector3 prevPos;

    // Start is called before the first frame update
    protected override void Start()
    {
        startingMaterial = GetComponentInChildren<MeshRenderer>().material;
        startPos = transform.position;
        prevPos = startPos;
        gameOver = false;
        inBasket = false;
        base.Start();
    }

    private void Update()
    {
        UpdatePathLength();

        /* If acorn is under map and not in the basket, reset acorn position */
        if (transform.position.y <= 0.0f && !inBasket)
        {
            GetComponentInChildren<Rigidbody>().useGravity = false;
            GetComponentInChildren<Rigidbody>().isKinematic = true;
            transform.position = startPos;
        }
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Basket") && !inBasket)
        {
            GetComponentInChildren<MeshRenderer>().material = startingMaterial;
            GetComponentInChildren<Rigidbody>().useGravity = false;
            GetComponentInChildren<Rigidbody>().isKinematic = true;
            while (Random.Range(0.0f, 1.0f) < 0.7f) ;
            BasketSceneController.addAcorn = true;
            inBasket = true;
            gameOver = true;

        }
    }

    void UpdatePathLength()
    {
        if (!gameOver)
        {
            tempPath = Mathf.Sqrt(Mathf.Pow(prevPos.x - transform.position.x, 2));
            tempPath += Mathf.Sqrt(Mathf.Pow(prevPos.y - transform.position.y, 2));
            tempPath += Mathf.Sqrt(Mathf.Pow(prevPos.z - transform.position.z, 2));
            pathLength += tempPath;
            prevPos = transform.position;
        }
    }



    public static float GetPathLength()
    {
        return pathLength;
    }

}
