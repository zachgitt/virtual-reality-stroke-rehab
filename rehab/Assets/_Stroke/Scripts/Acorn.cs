using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : OVRGrabbable
{
    public Material startingMaterial;
    private static float pathLength;
    private float tempPath;

    // Start is called before the first frame update
    protected override void Start()
    {
        startingMaterial = GetComponentInChildren<MeshRenderer>().material;
        pathLength = 0.0f;
        base.Start();
    }

    private void Update()
    {
        tempPath = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2));
        tempPath += Mathf.Sqrt(Mathf.Pow(transform.position.y, 2));
        tempPath += Mathf.Sqrt(Mathf.Pow(transform.position.z, 2));
        pathLength += tempPath;
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
        if (other.name.Equals("Basket"))
        {
            GetComponentInChildren<MeshRenderer>().material = startingMaterial;
            GetComponentInChildren<Rigidbody>().useGravity = false;
            GetComponentInChildren<Rigidbody>().isKinematic = true;
            BasketSceneController.addAcorn = true;
        }
    }

    public static float GetPathLength()
    {
        return pathLength;
    }

}
